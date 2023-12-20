using CiclogramWriter.Core;
using CiclogramWriter.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CiclogramWriter
{
	public partial class MainForm : Form
	{
		private Processor processor = new Processor();

		public MainForm()
		{
			InitializeComponent();

			tb_mp_sh.Text = processor.Fsh.ToString();
			tb_f_op.Text = processor.Fop.ToString();
			tb_ver_in_cache.Text = processor.ProbabilityAddToCache.ToString();
		}
		/// <summary>
		/// Метод добавления микропроцессора
		/// </summary>
		private void btn_add_pm_Click(object sender, EventArgs e)
		{
			var a_id_mp = processor.MPList.Select(x => x.Id).ToList();
			int id_mp = (a_id_mp != null && a_id_mp.Count > 0) ? a_id_mp.Max() + 1 : 1;
			var o_mp_new = new Microprocessor()
			{
				Id = id_mp,
				NumberOfController = (int)num_of_control.Value
			};

			tb_num_pm.Text = id_mp.ToString();
			processor.MPList.Add(o_mp_new);

			UpdateListCommand_RichTB();
		}
		/// <summary>
		/// Метод удаления микропроцессора
		/// </summary>
		private void btn_del_pm_Click(object sender, EventArgs e)
		{
			if (tb_num_pm.Text.Length > 0)
			{
				int id_mp = Int32.Parse(tb_num_pm.Text);
				var o_mp_del = processor.MPList.Where(x => x.Id == id_mp).FirstOrDefault();

				if (o_mp_del != null)
				{
					processor.MPList.Remove(o_mp_del);
					tb_num_pm.Text = string.Empty;

					UpdateListCommand_RichTB();

					MessageBox.Show($"Микропроцессор №{id_mp} успешно удален.", "Информация");
				}
				else
				{
					MessageBox.Show($"Микропроцессор №{id_mp} не найден.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MessageBox.Show("Для удаления укажите номер микропроцессора.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		/// <summary>
		/// Метод добавления команды в МП
		/// </summary>
		private void btn_add_command_Click(object sender, EventArgs e)
		{
			if (tb_num_pm.Text.Length == 0)
			{
				MessageBox.Show("Для добавления команды укажите номер микропроцессора.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			int id_mp = Int32.Parse(tb_num_pm.Text);
			var o_mp = processor.MPList.Where(x => x.Id == id_mp).FirstOrDefault();
			if (o_mp == null)
			{
				MessageBox.Show("Перед добавления команды должен быть создан микропроцессор.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var a_id_command = o_mp.CommandList.Select(x => x.Id).ToList();
			int id_command = (a_id_command != null && a_id_command.Count > 0) ? a_id_command.Max() + 1 : 1;

			var o_command = new Command()
			{
				Id = id_command,
				ExecutionStatus = ExecutionStatus.Inactive,
				IsCached = cb_in_cache.Checked,
				IsManagementOperation = cb_yo.Checked,
				NumberOfClockCycles = (int)num_count_tact.Value,
				Priority = 1
			};

			// Устанавливаем тип команды.
			if (o_command.IsCached && !o_command.IsManagementOperation)
			{
				o_command.CommandType = Enums.CommandType.Cache_False;
				o_command.Priority = 1;
			}
			else if (o_command.IsCached && o_command.IsManagementOperation)
			{
				o_command.CommandType = Enums.CommandType.Cache_MO;
				o_command.Priority = 1;
			}
			else if (!o_command.IsCached && !o_command.IsManagementOperation)
			{
				o_command.CommandType = Enums.CommandType.NotCache_False;
				o_command.Priority = 1;
			}
			else if (!o_command.IsCached && o_command.IsManagementOperation)
			{
				o_command.CommandType = Enums.CommandType.NotCache_MO;
				o_command.Priority = 2;
			}

			o_mp.CommandList.Add(o_command);

			UpdateListCommand_RichTB();
		}
		/// <summary>
		/// Метод изменения значения "f(МП) > f(СШ)"
		/// </summary>
		private void tb_mp_sh_TextChanged(object sender, EventArgs e)
		{
			string s_val = tb_mp_sh.Text;
			if (s_val.Length > 0)
			{
				processor.Fsh = Int32.Parse(s_val.Trim());
			}
			else
			{
				tb_mp_sh.Text = processor.Fsh.ToString();
			}
		}
		/// <summary>
		/// Метод изменения значения "F(ОП)"
		/// </summary>
		private void tb_f_op_TextChanged(object sender, EventArgs e)
		{
			string s_val = tb_f_op.Text;
			if (s_val.Length > 0)
			{
				processor.Fop = Int32.Parse(s_val.Trim());
			}
			else
			{
				tb_f_op.Text = processor.Fop.ToString();
			}
		}
		/// <summary>
		/// Метод изменения значения "Вероятность попадания в кеш"
		/// </summary>
		private void tb_ver_in_cache_TextChanged(object sender, EventArgs e)
		{
			string s_val = tb_ver_in_cache.Text;
			if (s_val.Length > 0)
			{
				processor.ProbabilityAddToCache = Int32.Parse(s_val.Trim());
			}
			else
			{
				tb_ver_in_cache.Text = processor.ProbabilityAddToCache.ToString();
			}
		}
		/// <summary>
		/// Метод обновления текстового поля "Список команд".
		/// </summary>
		private void UpdateListCommand_RichTB()
		{
			if (processor.MPList.Count == 0)
				return;

			rt_list_command.Text = string.Empty;

			foreach (var o_mp in processor.MPList)
			{
				string s_result = $"Микропроцессор №{o_mp.Id}\n";

				if (o_mp.CommandList.Count > 0)
				{
					foreach (var o_command in o_mp.CommandList)
					{
						switch (o_command.CommandType)
						{
							case Enums.CommandType.Cache_False:
								s_result += $"Команда №{o_command.Id} (Кеш; -) - {o_command.NumberOfClockCycles} тактов.\n";
								break;
							case Enums.CommandType.Cache_MO:
								s_result += $"Команда №{o_command.Id} (Кеш; У.О.) - {o_command.NumberOfClockCycles} тактов.\n";
								break;
							case Enums.CommandType.NotCache_False:
								s_result += $"Команда №{o_command.Id} (Не кеш; -) - {o_command.NumberOfClockCycles} тактов.\n";
								break;
							case Enums.CommandType.NotCache_MO:
								s_result += $"Команда №{o_command.Id} (Не кеш; У.О.) - {o_command.NumberOfClockCycles} тактов.\n";
								break;
						}
					}
				}

				rt_list_command.Text += s_result + "\n";
			}
		}
		/// <summary>
		/// Метод создания холста для циклограммы
		/// </summary>
		private void CreateChartCanvas()
		{
			var o_draw = new DrawChart();

			Bitmap o_bitm = new Bitmap(o_draw.WidthCanvas, o_draw.HeightCanvas);
			Graphics o_graphic = Graphics.FromImage(o_bitm);

			o_draw.DrawGrid(o_graphic);

			foreach (var o_mp in processor.MPList)
			{
				int i_start_x_kn = 0;
				int i_start_y_kn = 0;

				int i_start_x_kk = 0;
				int i_start_y_kk = 0;

				int i_start_x_request = 0;
				int i_start_y_request = 0;

				#region Контроллер и конвейер микропроцессора
				for (int i = 0; i < o_mp.NumberOfController; i++)
				{
					o_draw.DrawLine(o_graphic, $"k{i + 1}", out i_start_y_kn);
					o_draw.StepIndentLine += o_draw.IndentLine;
				}

				o_draw.DrawLine(o_graphic, "kk", out i_start_y_kk);
				o_draw.StepIndentLine += o_draw.IndentLine;
				#endregion

				var a_temp_command = o_mp.CommandList;

				bool in_progress = true;
				while (in_progress)
				{
					if (o_mp.RequestList.Count > 0)
					{
						var o_temp_request = o_mp.RequestList[0];

						switch (o_temp_request.Command.CommandType)
						{
							case Enums.CommandType.Cache_MO:
								{
									o_draw.DrawSystemBusKN(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, i_start_x_kn, i_start_y_kn, processor.Fsh, out int end_point_x);

									i_start_x_kn += end_point_x;

									o_mp.RequestList.Remove(o_temp_request);
									a_temp_command.Remove(o_temp_request.Command);

									break;
								}
							case Enums.CommandType.NotCache_False:
								{
									o_draw.DrawSystemBusKK(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, i_start_x_kk, i_start_y_kk, processor.Fop, out int end_point_x);

									i_start_x_kk += end_point_x;

									o_draw.DrawCacheKN(o_graphic, $"{o_temp_request.Command.Id}", i_start_x_kn, i_start_y_kn, out end_point_x);

									i_start_x_kn += end_point_x;

									o_draw.DrawMicroBusKN(o_graphic, o_temp_request.Command.NumberOfClockCycles, i_start_x_kn, i_start_y_kn, out end_point_x);

									i_start_x_kn += end_point_x;

									o_mp.RequestList.Remove(o_temp_request);
									a_temp_command.Remove(o_temp_request.Command);

									break;
								}
							case Enums.CommandType.NotCache_MO:
								{
									switch (o_temp_request.StateCommand)
									{
										case StateCommand.SystemBusKK:
											{
												o_draw.DrawSystemBusKK(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, i_start_x_kk, i_start_y_kk, processor.Fop, out int end_point_x);

												i_start_x_kk += end_point_x;

												o_temp_request.StateCommand = StateCommand.Decode;

												break;
											}
										case StateCommand.Decode:
											{
												o_draw.DrawCacheKN(o_graphic, $"{o_temp_request.Command.Id}", i_start_x_kn, i_start_y_kn, out int end_point_x);

												i_start_x_kn += end_point_x;

												var o_request = new Request()
												{
													Command = o_temp_request.Command,
													Priority = 1,
													StateCommand = StateCommand.SystemBusKN
												};

												o_mp.RequestList.Add(o_request);
												
												o_draw.DrawRequest(o_graphic, o_request.Command.Id.ToString(), i_start_x_kn, i_start_y_kn);

												o_mp.RequestList.Remove(o_temp_request);

												break;
											}
										case StateCommand.SystemBusKN:
											{
												o_draw.DrawSystemBusKN(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, i_start_x_kn, i_start_y_kn, processor.Fsh, out int end_point_x);

												i_start_x_kn += end_point_x;

												o_mp.RequestList.Remove(o_temp_request);
												a_temp_command.Remove(o_temp_request.Command);

												break;
											}
									}

									break;
								}
						}
					}

					if(a_temp_command.Count > 0)
					{
						var o_temp_command = a_temp_command[0];

						switch (o_temp_command.CommandType)
						{
							case Enums.CommandType.Cache_False:
								{
									o_draw.DrawCacheKN(o_graphic, $"{o_temp_command.Id}", i_start_x_kn, i_start_y_kn, out int end_point_x);

									i_start_x_kn += end_point_x;

									o_draw.DrawMicroBusKN(o_graphic, o_temp_command.NumberOfClockCycles, i_start_x_kn, i_start_y_kn, out end_point_x);

									i_start_x_kn += end_point_x;

									a_temp_command.Remove(o_temp_command);

									break;
								}
							case Enums.CommandType.Cache_MO:
								{
									o_draw.DrawCacheKN(o_graphic, $"{o_temp_command.Id}", i_start_x_kn, i_start_y_kn, out int end_point_x);
									
									i_start_x_kn += end_point_x;

									o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), i_start_x_kn, i_start_y_kn);

									var o_request = new Request()
									{
										Command = o_temp_command,
										Priority = 1,
										StateCommand = StateCommand.SystemBusKN
									};

									o_mp.RequestList.Add(o_request);
									a_temp_command.Remove(o_temp_command);

									break;
								}
							case Enums.CommandType.NotCache_False:
								{
									o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), i_start_x_kn, i_start_y_kn);

									var o_request = new Request()
									{
										Command = o_temp_command,
										Priority = 1,
										StateCommand = StateCommand.SystemBusKK
									};

									o_mp.RequestList.Add(o_request);
									a_temp_command.Remove(o_temp_command);

									break;
								}
							case Enums.CommandType.NotCache_MO:
								{
									o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), i_start_x_kn, i_start_y_kn);

									var o_request = new Request()
									{
										Command = o_temp_command,
										Priority = 1,
										StateCommand = StateCommand.SystemBusKK
									};

									o_mp.RequestList.Add(o_request);
									a_temp_command.Remove(o_temp_command);

									break;
								}
						}
					}

					// Выходим из цикла, когда нет заявок и все команды выполнены
					if (o_mp.RequestList.Count == 0 && a_temp_command.Count == 0)
					{
						in_progress = false;
					}
				}
			}

			pb_canvas.Image = o_bitm;
		}
		/// <summary>
		/// Метод отрисовки графика
		/// </summary>
		private void btn_draw_chart_Click(object sender, EventArgs e)
		{
			if (processor.MPList.Count == 0)
			{
				MessageBox.Show("Добавьте микропроцессор.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			CreateChartCanvas();
		}
		/// <summary>
		/// Метод автоматической генерации команд микропроцессора
		/// </summary>
		private void brn_gen_Click(object sender, EventArgs e)
		{
			if (processor.MPList.Count == 0)
			{
				MessageBox.Show("Добавьте микропроцессор.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			List<Command> a_gen_command = new List<Command>() {
				new Command()
				{
					Id = 1,
					CommandType = Enums.CommandType.NotCache_MO,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = false,
					IsManagementOperation = true,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 2,
					CommandType = Enums.CommandType.Cache_False,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = true,
					IsManagementOperation = false,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 3,
					CommandType = Enums.CommandType.NotCache_False,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = false,
					IsManagementOperation = false,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 4,
					CommandType = Enums.CommandType.NotCache_MO,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = false,
					IsManagementOperation = true,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 5,
					CommandType = Enums.CommandType.Cache_MO,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = true,
					IsManagementOperation = true,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 6,
					CommandType = Enums.CommandType.Cache_MO,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = true,
					IsManagementOperation = true,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 7,
					CommandType = Enums.CommandType.NotCache_False,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = false,
					IsManagementOperation = false,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 8,
					CommandType = Enums.CommandType.NotCache_MO,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = false,
					IsManagementOperation = true,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 9,
					CommandType = Enums.CommandType.Cache_False,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = true,
					IsManagementOperation = false,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 10,
					CommandType = Enums.CommandType.Cache_MO,
					ExecutionStatus = ExecutionStatus.Inactive,
					IsCached = true,
					IsManagementOperation = true,
					NumberOfClockCycles = 1,
				}
			};

			int id_mp = Int32.Parse(tb_num_pm.Text);
			var o_mp = processor.MPList.Where(x => x.Id == id_mp).FirstOrDefault();
			o_mp.CommandList.AddRange(a_gen_command);

			UpdateListCommand_RichTB();
		}
	}
}
