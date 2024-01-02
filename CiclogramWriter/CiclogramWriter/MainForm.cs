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
			var o_mp_new = new Microprocessor(id_mp);

			for(int i=0; i< (int)num_of_control.Value; i++)
			{
				o_mp_new.ControllerList.Add(new ExecutionVector(i+1));
			}

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
				NumberOfClockCycles = (int)num_count_tact.Value
			};

			// Устанавливаем тип команды.
			if (cb_dma.Checked)
			{
				o_command.CommandType = Enums.CommandType.DMA;

				if (o_mp.DirectMemoryAccess == null)
				{
					o_mp.DirectMemoryAccess = new ExecutionVector(1);
				}
			}
			else
			{
				if (cb_in_cache.Checked && !cb_yo.Checked)
				{
					o_command.CommandType = Enums.CommandType.Cache_False;
				}
				else if (cb_in_cache.Checked && cb_yo.Checked)
				{
					o_command.CommandType = Enums.CommandType.Cache_MO;
				}
				else if (!cb_in_cache.Checked && !cb_yo.Checked)
				{
					o_command.CommandType = Enums.CommandType.NotCache_False;
				}
				else if (!cb_in_cache.Checked && cb_yo.Checked)
				{
					o_command.CommandType = Enums.CommandType.NotCache_MO;
				}
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
							case Enums.CommandType.DMA:
								s_result += $"Команда №{o_command.Id} (DMA) - {o_command.NumberOfClockCycles} тактов.\n";
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
				DrawMicroprocessor(o_mp, o_draw, o_graphic);
			}

			pb_canvas.Image = o_bitm;
		}

		/// <summary>
		/// Метод отрисовки выполнения команд в микропроцессоре.
		/// </summary>
		/// <param name="o_mp">Микропроцессор</param>
		/// <param name="o_draw">Объект отрисовки элементов</param>
		/// <param name="o_graphic"></param>
		private void DrawMicroprocessor(Microprocessor o_mp, DrawChart o_draw, Graphics o_graphic)
		{
			#region Контроллер, конвейер и DMA микропроцессора
			foreach(var o_controller in o_mp.ControllerList)
			{
				o_draw.DrawLine(o_graphic, $"k{o_controller.Id}", out int startPointY_kn);
				o_controller.StartPointY = startPointY_kn;
				o_draw.StepIndentLine += o_draw.IndentLine;
			}

			o_draw.DrawLine(o_graphic, "kk", out int startPointY_kk);
			o_mp.Conveyor.StartPointY = startPointY_kk;
			o_draw.StepIndentLine += o_draw.IndentLine;

			if (o_mp.DirectMemoryAccess != null)
			{
				o_draw.DrawLine(o_graphic, "DMA", out int startPointY_dma);
				o_mp.DirectMemoryAccess.StartPointY = startPointY_dma;
				o_draw.StepIndentLine += o_draw.IndentLine;
			}
			#endregion

			// Счетчик тактов (необходим для получения информации, как долго будет занята системная шина)
			int i_tact_time = 0;

			int i_count = 0;

			// Флаг - находимся в режиме отрисовки циклограммы
			bool in_progress = true;
			
			while (in_progress)
			{
				// Выходим из цикла, когда нет заявок и все команды выполнены
				if (o_mp.RequestList.Count == 0 && o_mp.CommandList.Count == 0)
				{
					in_progress = false;
				}

				var o_controller = GetFreeController(o_mp.ControllerList);

				// Выбираем заявки
				if (o_mp.RequestList.Count > 0 && o_mp.Conveyor.IsFree)
				{
					// Выбираются заявки с приоритетом (Если висист [кеш; УО] или [не кеш; уо])
					var a_request_priority = o_mp.RequestList
						.Where(x => x.Command.CommandType == Enums.CommandType.Cache_MO
							|| (x.Command.CommandType == Enums.CommandType.NotCache_MO && x.StateCommand == StateCommand.SystemBusKN))
						.OrderBy(x => x.Command.Id).ToList();

					var o_temp_request = o_mp.RequestList[0];
					if (a_request_priority.Count > 0)
					{
						o_temp_request = a_request_priority[0];
					}

					switch (o_temp_request.Command.CommandType)
					{
						case Enums.CommandType.Cache_MO:
							{
								o_draw.DrawSystemBusKN(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY, processor.Fsh);

								o_controller.NumberOfTact += o_temp_request.Command.NumberOfClockCycles * processor.Fsh;

								o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;

								o_mp.RequestList.Remove(o_temp_request);

								i_tact_time = o_controller.NumberOfTact;

								break;
							}
						case Enums.CommandType.NotCache_False:
							{
								switch (o_temp_request.StateCommand)
								{
									case StateCommand.SystemBusKK:
										{
											o_draw.DrawSystemBusKK(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_mp.Conveyor.NumberOfTact * DrawChart.SquareSize, o_mp.Conveyor.StartPointY, processor.Fop);

											o_mp.Conveyor.NumberOfTact += 2 * o_temp_request.Command.NumberOfClockCycles * processor.Fop;

											o_temp_request.StateCommand = StateCommand.Decode;

											break;
										}
									case StateCommand.Decode:
										{
											o_draw.DrawCacheKN(o_graphic, $"{o_temp_request.Command.Id}", o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

											o_controller.NumberOfTact += 1;

											o_draw.DrawMicroBusKN(o_graphic, o_temp_request.Command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

											o_controller.NumberOfTact += o_temp_request.Command.NumberOfClockCycles;

											o_mp.RequestList.Remove(o_temp_request);

											i_tact_time = o_controller.NumberOfTact;

											break;
										}
								}

								break;
							}
						case Enums.CommandType.NotCache_MO:
							{
								switch (o_temp_request.StateCommand)
								{
									case StateCommand.SystemBusKK:
										{
											o_draw.DrawSystemBusKK(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_mp.Conveyor.NumberOfTact * DrawChart.SquareSize, o_mp.Conveyor.StartPointY, processor.Fop);

											o_mp.Conveyor.NumberOfTact += 2 * o_temp_request.Command.NumberOfClockCycles * processor.Fop;

											o_temp_request.StateCommand = StateCommand.Decode;

											break;
										}
									case StateCommand.Decode:
										{
											o_draw.DrawCacheKN(o_graphic, $"{o_temp_request.Command.Id}", o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

											o_controller.NumberOfTact += 1;

											var o_request = new Request()
											{
												Command = o_temp_request.Command,
												StateCommand = StateCommand.SystemBusKN
											};

											o_mp.RequestList.Add(o_request);

											o_controller.StartRequestPointY = (o_controller.StartRequestPointX == o_controller.NumberOfTact * DrawChart.SquareSize)
												? o_controller.StartRequestPointY - DrawChart.SquareSize
												: o_controller.StartPointY;

											o_draw.DrawRequest(o_graphic, o_request.Command.Id.ToString(), o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartRequestPointY);
											o_controller.StartRequestPointX = o_controller.NumberOfTact * DrawChart.SquareSize;

											o_mp.RequestList.Remove(o_temp_request);

											i_tact_time = o_controller.NumberOfTact;

											break;
										}
									case StateCommand.SystemBusKN:
										{
											o_draw.DrawSystemBusKN(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY, processor.Fsh);

											o_controller.NumberOfTact += o_temp_request.Command.NumberOfClockCycles * processor.Fsh;

											o_mp.RequestList.Remove(o_temp_request);

											o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;

											i_tact_time = o_controller.NumberOfTact;

											break;
										}
								}

								break;
							}
						case Enums.CommandType.DMA:
							{
								o_draw.DrawSystemBusKK(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_mp.DirectMemoryAccess.NumberOfTact * DrawChart.SquareSize, o_mp.DirectMemoryAccess.StartPointY, processor.Fop);

								o_mp.DirectMemoryAccess.NumberOfTact += 2 * o_temp_request.Command.NumberOfClockCycles * processor.Fop;

								break;
							}
					}
				}

				// Выбираем команды
				if (o_mp.CommandList.Count > 0)
				{
					var o_temp_command = o_mp.CommandList[0];
					o_temp_command.ControllerId = o_controller.Id;

					switch (o_temp_command.CommandType)
					{
						case Enums.CommandType.Cache_False:
							{
								o_draw.DrawCacheKN(o_graphic, $"{o_temp_command.Id}", o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

								o_controller.NumberOfTact += 1;

								o_draw.DrawMicroBusKN(o_graphic, o_temp_command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

								o_controller.NumberOfTact += o_temp_command.NumberOfClockCycles;

								o_mp.CommandList.Remove(o_temp_command);

								if (o_mp.Conveyor.IsFree)
								{
									i_tact_time = o_controller.NumberOfTact;
								}

								break;
							}
						case Enums.CommandType.Cache_MO:
							{
								o_draw.DrawCacheKN(o_graphic, $"{o_temp_command.Id}", o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

								o_controller.NumberOfTact += 1;

								o_controller.StartRequestPointY = (o_mp.RequestList.Count > 0 && o_controller.StartRequestPointX == o_controller.NumberOfTact * DrawChart.SquareSize)
									? o_controller.StartRequestPointY - DrawChart.SquareSize
									: o_controller.StartPointY;

								o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartRequestPointY);

								o_controller.StartRequestPointX = o_controller.NumberOfTact * DrawChart.SquareSize;

								var o_request = new Request()
								{
									Command = o_temp_command,
									StateCommand = StateCommand.SystemBusKN
								};

								o_mp.RequestList.Add(o_request);

								if (o_mp.Conveyor.NumberOfTact <= o_controller.NumberOfTact && i_count == 0)
								{
									o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
								}

								o_mp.CommandList.Remove(o_temp_command);

								if (o_mp.Conveyor.IsFree)
								{
									i_tact_time = o_controller.NumberOfTact;
								}

								break;
							}
						case Enums.CommandType.NotCache_False:
							{
								o_controller.StartRequestPointY = (o_mp.RequestList.Count > 0 && o_controller.StartRequestPointX == o_controller.NumberOfTact * DrawChart.SquareSize)
									? o_controller.StartRequestPointY - DrawChart.SquareSize
									: o_controller.StartPointY;

								o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartRequestPointY);

								o_controller.StartRequestPointX = o_controller.NumberOfTact * DrawChart.SquareSize;

								var o_request = new Request()
								{
									Command = o_temp_command,
									StateCommand = StateCommand.SystemBusKK
								};

								o_mp.RequestList.Add(o_request);

								if (o_mp.Conveyor.NumberOfTact <= o_controller.NumberOfTact && i_count == 0)
								{
									o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
								}

								o_mp.CommandList.Remove(o_temp_command);

								break;
							}
						case Enums.CommandType.NotCache_MO:
							{
								o_controller.StartRequestPointY = (o_mp.RequestList.Count > 0 && o_controller.StartRequestPointX == o_controller.NumberOfTact * DrawChart.SquareSize)
									? o_controller.StartRequestPointY - DrawChart.SquareSize
									: o_controller.StartPointY;

								o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartRequestPointY);

								o_controller.StartRequestPointX = o_controller.NumberOfTact * DrawChart.SquareSize;

								var o_request = new Request()
								{
									Command = o_temp_command,
									StateCommand = StateCommand.SystemBusKK
								};

								o_mp.RequestList.Add(o_request);

								if (o_mp.Conveyor.NumberOfTact <= o_controller.NumberOfTact && i_count == 0)
								{
									o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
								}

								o_mp.CommandList.Remove(o_temp_command);

								break;
							}
						case Enums.CommandType.DMA:
							{
								o_controller.StartRequestPointY = (o_mp.RequestList.Count > 0 && o_controller.StartRequestPointX == o_controller.NumberOfTact * DrawChart.SquareSize)
									? o_controller.StartRequestPointY - DrawChart.SquareSize
									: o_controller.StartPointY;

								o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartRequestPointY);

								o_controller.StartRequestPointX = o_controller.NumberOfTact * DrawChart.SquareSize;

								var o_request = new Request()
								{
									Command = o_temp_command,
								};

								o_mp.RequestList.Add(o_request);

								if (o_mp.DirectMemoryAccess.NumberOfTact <= o_controller.NumberOfTact && i_count == 0)
								{
									o_mp.DirectMemoryAccess.NumberOfTact = o_controller.NumberOfTact;
								}

								o_mp.CommandList.Remove(o_temp_command);

								break;
							}
					}
				}

				// Проверряем занята ли системная шина
				if (o_mp.Conveyor.NumberOfTact > i_tact_time)
				{
					o_mp.Conveyor.IsFree = false;
					i_tact_time++;
				}
				else
				{
					o_mp.Conveyor.IsFree = true;

					if (i_tact_time > o_controller.NumberOfTact)
					{
						o_controller.NumberOfTact = i_tact_time;
					}
				}

				i_count++;
			}
		}
		/// <summary>
		/// Метод получения свободного контроллера.
		/// </summary>
		/// <param name="a_controller">Список контроллеров</param>
		private ExecutionVector GetFreeController(List<ExecutionVector> a_controller)
		{
			// Выбираем свободные контроллеры.
			var a_cont_free = a_controller.Where(x => x.IsFree).OrderBy(x => x.Id).ToList();
			// Если все заняты выбираем первый
			if (a_cont_free == null)
			{
				return a_controller.Where(x => x.Id == 1).FirstOrDefault();
			}
			else
			{
				// Выбираем контроллер, у которого меньше всего занятого места.
				ExecutionVector o_res_controller = a_cont_free.OrderBy(x => x.Id).FirstOrDefault();
				int i_min = o_res_controller.NumberOfTact;
				foreach(var o_cont in a_cont_free)
				{
					if (i_min < o_cont.NumberOfTact)
					{
						i_min = o_cont.NumberOfTact;
						o_res_controller = o_cont;
					}
				}

				return o_res_controller;
			}
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

			List<Command> a_gen_command = new List<Command>();

			for (int i = 0; i < 100; i++)
			{
				var o_command = new Command()
				{
					Id = (i + 1),
					NumberOfClockCycles = new Random().Next(1,5)
				};

				switch(new Random().Next(1, 4))
				{
					case 1:
						{
							o_command.CommandType = Enums.CommandType.Cache_False;
							break;
						}
					case 2:
						{
							o_command.CommandType = Enums.CommandType.Cache_MO;
							break;
						}
					case 3:
						{
							o_command.CommandType = Enums.CommandType.NotCache_False;
							break;
						}
					case 4:
						{
							o_command.CommandType = Enums.CommandType.NotCache_MO;
							break;
						}
				}

				a_gen_command.Add(o_command);
			}

			int id_mp = Int32.Parse(tb_num_pm.Text);
			var o_mp = processor.MPList.Where(x => x.Id == id_mp).FirstOrDefault();
			o_mp.CommandList.AddRange(a_gen_command);

			UpdateListCommand_RichTB();
		}
	}
}
