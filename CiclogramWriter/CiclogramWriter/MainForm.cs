using CiclogramWriter.Core;
using CiclogramWriter.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
				o_mp_new.ControllerList.Add(new Controller(i+1));
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
					o_mp.DirectMemoryAccess = new DirectMemoryAccess(1);
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

			// Флаг - находимся в режиме отрисовки циклограммы
			bool in_progress = true;
			
			while (in_progress)
			{
				// Выходим из цикла, когда нет заявок и все команды выполнены
				if (o_mp.RequestList.Count == 0 && o_mp.CommandList.Count == 0)
				{
					in_progress = false;
				}

				var o_controller = GetFreeController(o_mp.ControllerList, o_mp);

				// Выбираем заявки
				if (o_mp.RequestList.Count > 0 && o_mp.Conveyor.IsFree)
				{
					WorkWithRequest_DMA(o_mp, o_controller, o_draw, o_graphic);

					
					WorkWithRequest(o_mp, o_controller, o_draw, o_graphic);
				}

				o_controller = GetFreeController(o_mp.ControllerList, o_mp);

				// Проверяется, что в контроллере есть заявка.
				// Если существует, тогда выполняется холостой прогон.
				if (o_mp.RequestList.Count > 0 && o_controller!=null)
				{
					int num_of_request_in_controller = o_mp.RequestList
					.Where(x => x.Command.ControllerId == o_controller.Id && (
							x.Command.CommandType == Enums.CommandType.Cache_MO
							|| (x.Command.CommandType == Enums.CommandType.NotCache_MO && x.StateCommand == StateCommand.SystemBusKN)
						)).Count();
					if (num_of_request_in_controller > 0)
					{
						continue;
					}
				}

				// Выбираем команды
				if (o_mp.CommandList.Count > 0)
				{
					WorkWithCommand(o_mp, o_controller, o_draw, o_graphic);
				}

				// Проверяем занята ли системная шина
				if (o_mp.Conveyor.NumberOfTact > o_mp.NumberOfTactTime)
				{
					o_mp.Conveyor.IsFree = false;
					o_mp.NumberOfTactTime++;
				}
				else
				{
					// Разблокируем контроллеры, когда освободится системная шина
					// должно выполнится как только, она освободится, а не постоянно. 
					if (!o_mp.Conveyor.IsFree)
					{
						o_mp.Conveyor.IsFree = true;

						// Устанавливается такт выполнения системной шины
						var a_controller = o_mp.ControllerList.ToList();
						foreach (var o_contr_set_free in a_controller)
						{
							o_contr_set_free.IsFree = true;
							o_contr_set_free.NumberOfTact = o_mp.NumberOfTactTime;
						}
					}
					else
					{
						var a_controller = o_mp.ControllerList.Where(x=> !x.IsFree).ToList();
						foreach (var o_contr_set_free in a_controller)
						{
							o_contr_set_free.IsFree = true;
						}
					}
				}

				o_mp.NumberOfСount++;
			}
		}

		/// <summary>
		/// Метод работы с заявками (отрисовка) ДМА
		/// </summary>
		/// <param name="o_mp">Микропроцессор</param>
		/// <param name="o_controller">Контроллер</param>
		/// <param name="o_draw">Объект отрисовки элементов</param>
		/// <param name="o_graphic"></param>
		private void WorkWithRequest_DMA(Microprocessor o_mp, Controller o_controller, DrawChart o_draw, Graphics o_graphic)
		{
			if (o_controller == null)
			{
				return;
			}

			var a_request = o_mp.RequestList
				.Where(x => x.Command.ControllerId == o_controller.Id && x.Command.CommandType == Enums.CommandType.DMA)
				.OrderBy(x => x.Command.Id).ToList();

			if (a_request.Count > 0)
			{
				var o_temp_request = a_request.OrderBy(x => x.Command.Id).FirstOrDefault();

				o_draw.DrawSystemBusKK(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_mp.DirectMemoryAccess.NumberOfTact * DrawChart.SquareSize, o_mp.DirectMemoryAccess.StartPointY, processor.Fop);

				o_mp.DirectMemoryAccess.NumberOfTact += 2 * o_temp_request.Command.NumberOfClockCycles * processor.Fop;

				o_mp.RequestList.Remove(o_temp_request);
			}
			else
			{
				return;
			}
		}

		/// <summary>
		/// Метод работы с заявками (отрисовка)
		/// </summary>
		/// <param name="o_mp">Микропроцессор</param>
		/// <param name="o_controller">Контроллер</param>
		/// <param name="o_draw">Объект отрисовки элементов</param>
		/// <param name="o_graphic"></param>
		private void WorkWithRequest(Microprocessor o_mp, Controller o_controller, DrawChart o_draw, Graphics o_graphic)
		{
			if(o_controller == null)
			{
				return;
			}

			// Выбираются заявки с приоритетом (Если висист [кеш; УО] или [не кеш; уо])
			var a_request_priority = o_mp.RequestList
				.Where(x => x.Command.ControllerId == o_controller.Id && (
						x.Command.CommandType == Enums.CommandType.Cache_MO 
						|| (x.Command.CommandType == Enums.CommandType.NotCache_MO && x.StateCommand == StateCommand.SystemBusKN)
					))
				.OrderBy(x => x.Command.Id).ToList();

			var o_temp_request = o_mp.RequestList.Where(x => x.Command.ControllerId == o_controller.Id)
				.OrderBy(x => x.Command.Id).FirstOrDefault();

			if(o_temp_request == null)
			{
				return;
			}

			if (a_request_priority.Count > 0)
			{
				o_temp_request = a_request_priority.OrderBy(x => x.Command.Id).FirstOrDefault();
			}

			switch (o_temp_request.Command.CommandType)
			{
				case Enums.CommandType.Cache_MO:
					{
						// Проверяем что системная шина свободна, устанавливаем актуальное значение.
						if (o_mp.TactWhenSystemBusIsFree > o_controller.NumberOfTact)
						{
							o_controller.NumberOfTact = o_mp.TactWhenSystemBusIsFree;
						}

						o_draw.DrawSystemBusKN(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY, processor.Fsh);
						
						o_controller.NumberOfTact += o_temp_request.Command.NumberOfClockCycles * processor.Fsh;
						
						// Присваиваю такт когда системная шина освободится.
						o_mp.TactWhenSystemBusIsFree = o_controller.NumberOfTact;

						// Записывается последний максимальный показатель
						if (o_mp.Conveyor.NumberOfTact < o_controller.NumberOfTact)
						{
							o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
						}
						
						o_mp.RequestList.Remove(o_temp_request);

						if (o_mp.NumberOfTactTime < o_controller.NumberOfTact)
						{
							o_mp.NumberOfTactTime = o_controller.NumberOfTact;
						}

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

									if (o_mp.NumberOfTactTime < o_controller.NumberOfTact)
									{
										o_mp.NumberOfTactTime = o_controller.NumberOfTact;
									}

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

									if (o_mp.NumberOfTactTime < o_controller.NumberOfTact)
									{
										o_mp.NumberOfTactTime = o_controller.NumberOfTact;
									}

									// Блокируем контроллер до выполнения команды У.О.
									o_controller.IsFree = false;

									break;
								}
							case StateCommand.SystemBusKN:
								{
									// Проверяем что системная шина свободна, устанавливаем актуальное значение.
									if (o_mp.TactWhenSystemBusIsFree > o_controller.NumberOfTact)
									{
										o_controller.NumberOfTact = o_mp.TactWhenSystemBusIsFree;
									}

									o_draw.DrawSystemBusKN(o_graphic, $"{o_temp_request.Command.Id}", o_temp_request.Command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY, processor.Fsh);

									o_controller.NumberOfTact += o_temp_request.Command.NumberOfClockCycles * processor.Fsh;
									
									// Присваиваю такт когда системная шина освободится.
									o_mp.TactWhenSystemBusIsFree = o_controller.NumberOfTact;

									o_mp.RequestList.Remove(o_temp_request);

									// Записывается последний максимальный показатель
									if (o_mp.Conveyor.NumberOfTact < o_controller.NumberOfTact)
									{
										o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
									}

									if (o_mp.NumberOfTactTime < o_controller.NumberOfTact)
									{
										o_mp.NumberOfTactTime = o_controller.NumberOfTact;
									}

									break;
								}
						}

						break;
					}
			}
		}
		/// <summary>
		/// Метод работы с командами (отрисовка)
		/// </summary>
		/// <param name="o_mp">Микропроцессор</param>
		/// <param name="o_controller">Контроллер</param>
		/// <param name="o_draw">Объект отрисовки элементов</param>
		/// <param name="o_graphic"></param>
		private void WorkWithCommand(Microprocessor o_mp, Controller o_controller, DrawChart o_draw, Graphics o_graphic)
		{
			var o_temp_command = o_mp.CommandList.OrderBy(x=> x.Id).FirstOrDefault();
			o_temp_command.ControllerId = o_controller.Id;

			var a_request_list = o_mp.RequestList.Where(x => x.Command.ControllerId == o_controller.Id).ToList();

			switch (o_temp_command.CommandType)
			{
				case Enums.CommandType.Cache_False:
					{
						o_draw.DrawCacheKN(o_graphic, $"{o_temp_command.Id}", o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

						o_controller.NumberOfTact += 1;

						o_draw.DrawMicroBusKN(o_graphic, o_temp_command.NumberOfClockCycles, o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

						o_controller.NumberOfTact += o_temp_command.NumberOfClockCycles;

						if (o_mp.Conveyor.IsFree)
						{
							o_mp.NumberOfTactTime = o_controller.NumberOfTact;
						}

						break;
					}
				case Enums.CommandType.Cache_MO:
					{
						o_draw.DrawCacheKN(o_graphic, $"{o_temp_command.Id}", o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartPointY);

						o_controller.NumberOfTact += 1;

						DrawVerticalLineRequest(a_request_list, o_temp_command, o_controller, o_draw, o_graphic);

						var o_request = new Request()
						{
							Command = o_temp_command,
							StateCommand = StateCommand.SystemBusKN
						};

						o_mp.RequestList.Add(o_request);

						if (o_mp.Conveyor.NumberOfTact <= o_controller.NumberOfTact && o_mp.NumberOfСount == 0)
						{
							o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
						}

						if (o_mp.Conveyor.IsFree)
						{
							o_mp.NumberOfTactTime = o_controller.NumberOfTact;
						}

						// Блокируем контроллер до выполнения команды У.О.
						o_controller.IsFree = false;

						break;
					}
				case Enums.CommandType.NotCache_False:
					{
						DrawVerticalLineRequest(a_request_list, o_temp_command, o_controller, o_draw, o_graphic);

						var o_request = new Request()
						{
							Command = o_temp_command,
							StateCommand = StateCommand.SystemBusKK
						};

						o_mp.RequestList.Add(o_request);

						if (o_mp.Conveyor.NumberOfTact <= o_controller.NumberOfTact && o_mp.NumberOfСount == 0)
						{
							o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
						}

						break;
					}
				case Enums.CommandType.NotCache_MO:
					{
						DrawVerticalLineRequest(a_request_list, o_temp_command, o_controller, o_draw, o_graphic);

						var o_request = new Request()
						{
							Command = o_temp_command,
							StateCommand = StateCommand.SystemBusKK
						};

						o_mp.RequestList.Add(o_request);

						if (o_mp.Conveyor.NumberOfTact <= o_controller.NumberOfTact && o_mp.NumberOfСount == 0)
						{
							o_mp.Conveyor.NumberOfTact = o_controller.NumberOfTact;
						}

						break;
					}
				case Enums.CommandType.DMA:
					{
						DrawVerticalLineRequest(a_request_list, o_temp_command, o_controller, o_draw, o_graphic);

						var o_request = new Request()
						{
							Command = o_temp_command
						};

						o_mp.RequestList.Add(o_request);

						if (o_mp.DirectMemoryAccess.NumberOfTact <= o_controller.NumberOfTact && o_mp.NumberOfСount == 0)
						{
							o_mp.DirectMemoryAccess.NumberOfTact = o_controller.NumberOfTact;
						}

						break;
					}
			}

			o_mp.CommandList.Remove(o_temp_command);
		}
		/// <summary>
		/// Метод получения свободного контроллера.
		/// </summary>
		/// <param name="a_controller">Список контроллеров</param>
		private Controller GetFreeController(List<Controller> a_controller, Microprocessor o_mp)
		{
			// Выбираем свободные контроллеры.
			var a_cont_free = a_controller.Where(x => x.IsFree).OrderBy(x => x.Id).ToList();
			// Если все заняты выбираем первый
			if (a_cont_free.Count == 0)
			{
				return a_controller.Where(x => x.Id == 1).FirstOrDefault();
			}
			else
			{
				// Выбираем контроллер, у которого меньше всего занятого места.
				Controller o_res_controller = a_cont_free.OrderBy(x => x.NumberOfTact).FirstOrDefault();

				if (o_mp.RequestList.Count == 0 && o_mp.CommandList.Count == 0)
				{
					return o_res_controller;
				}

				int num_of_request_in_list = o_mp.RequestList
					.Where(x => x.Command.ControllerId == o_res_controller.Id).Count();

				// Проверяем, есть ли у контроллера заявки и находятся ли команды в очереди выполнения в МП.
				if (num_of_request_in_list == 0 && o_mp.CommandList.Count == 0)
				{
					// Выбирается контроллер с большим количеством заявок.
					var o_temp_controller = o_mp.RequestList
						.GroupBy(grp => grp.Command.ControllerId)
						.Select(grp => new {
							ControllerId = grp.Key,
							NumOfCommand = grp.Count()
						}).OrderBy(x=> x.NumOfCommand).FirstOrDefault();

					o_res_controller = a_cont_free.Where(x=> x.Id == o_temp_controller.ControllerId).FirstOrDefault();
				}

				return o_res_controller;
			}
		}
		/// <summary>
		/// Метод отрисовки вертикальной линии с обозначением номера заявки.
		/// </summary>
		/// <param name="a_request_list">Список заявок</param>
		/// <param name="o_temp_command">Команда</param>
		/// <param name="o_controller">Контроллер</param>
		/// <param name="o_draw">Объект отрисовки элементов</param>
		/// <param name="o_graphic"></param>
		private void DrawVerticalLineRequest(List<Request> a_request_list, Command o_temp_command, Controller o_controller, DrawChart o_draw, Graphics o_graphic) {
			o_controller.StartRequestPointY = (a_request_list.Count > 0 && o_controller.StartRequestPointX == o_controller.NumberOfTact * DrawChart.SquareSize)
				? o_controller.StartRequestPointY - DrawChart.SquareSize
				: o_controller.StartPointY;

			o_draw.DrawRequest(o_graphic, o_temp_command.Id.ToString(), o_controller.NumberOfTact * DrawChart.SquareSize, o_controller.StartRequestPointY);

			o_controller.StartRequestPointX = o_controller.NumberOfTact * DrawChart.SquareSize;
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
			List<Command> a_gen_command = new List<Command>() {
				new Command()
				{
					Id = 1,
					CommandType = Enums.CommandType.DMA,
					NumberOfClockCycles = 4,
				},
				new Command()
				{
					Id = 2,
					CommandType = Enums.CommandType.NotCache_MO,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 3,
					CommandType = Enums.CommandType.Cache_False,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 4,
					CommandType = Enums.CommandType.NotCache_False,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 5,
					CommandType = Enums.CommandType.NotCache_MO,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 6,
					CommandType = Enums.CommandType.Cache_MO,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 7,
					CommandType = Enums.CommandType.NotCache_False,
					NumberOfClockCycles = 1,
				},
				new Command()
				{
					Id = 8,
					CommandType = Enums.CommandType.NotCache_MO,
					NumberOfClockCycles = 2,
				},
				new Command()
				{
					Id = 9,
					CommandType = Enums.CommandType.Cache_False,
					NumberOfClockCycles = 2,
				}
			};

			if (processor.MPList.Count == 0)
			{
				MessageBox.Show("Добавьте микропроцессор.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//List<Command> a_gen_command = new List<Command>();

			//for (int i = 0; i < 100; i++)
			//{
			//	var o_command = new Command()
			//	{
			//		Id = (i + 1),
			//		NumberOfClockCycles = new Random().Next(1,5)
			//	};

			//	switch(new Random().Next(1, 4))
			//	{
			//		case 1:
			//			{
			//				o_command.CommandType = Enums.CommandType.Cache_False;
			//				break;
			//			}
			//		case 2:
			//			{
			//				o_command.CommandType = Enums.CommandType.Cache_MO;
			//				break;
			//			}
			//		case 3:
			//			{
			//				o_command.CommandType = Enums.CommandType.NotCache_False;
			//				break;
			//			}
			//		case 4:
			//			{
			//				o_command.CommandType = Enums.CommandType.NotCache_MO;
			//				break;
			//			}
			//	}

			//	a_gen_command.Add(o_command);
			//}

			int id_mp = Int32.Parse(tb_num_pm.Text);
			var o_mp = processor.MPList.Where(x => x.Id == id_mp).FirstOrDefault();
			o_mp.CommandList.AddRange(a_gen_command);

			bool draw_dma = o_mp.CommandList.Where(x => x.CommandType == Enums.CommandType.DMA).Count() > 0;
			if (draw_dma)
			{
				o_mp.DirectMemoryAccess = new DirectMemoryAccess(1);
			}

			UpdateListCommand_RichTB();
		}
	}
}
