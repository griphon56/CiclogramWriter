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
			int id_mp = (a_id_mp!=null && a_id_mp.Count>0) ? a_id_mp.Max() + 1 : 1;
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
			if(tb_num_pm.Text.Length > 0)
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
			if(tb_num_pm.Text.Length == 0)
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
			if(o_command.IsCached && !o_command.IsManagementOperation)
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
			// Шаг для отрисовки квадрата.
			int size_step = 20;
			int size_width = 950;
			int size_height = 850;

			Bitmap o_bitm = new Bitmap(size_width, size_height);
			Graphics o_graphic = Graphics.FromImage(o_bitm);

			#region Настройка шрифта
			Font drawFont = new Font("Calibri", 10);
			SolidBrush drawBrush = new SolidBrush(Color.Black);
			StringFormat drawFormat = new StringFormat();
			drawFormat.Alignment = StringAlignment.Near;
			#endregion

			#region Сетка графика
			// Вертикальные линии
			int step = 0;
			for (int i = 0; i < 100; i++)
			{
				o_graphic.DrawLine(new Pen(Color.Silver), step, 0, step, size_height);
				step += size_step;
			}

			// Горизонтальные линии
			step = 0;
			for (int i = 0; i < 100; i++)
			{
				o_graphic.DrawLine(new Pen(Color.Silver), 0, step, size_width, step);
				step += size_step;
			}
			#endregion

			int offset_line = size_step * 5;
			int step_line_mp = size_step * 5;
			foreach(var o_mp in processor.MPList)
			{
				int i_start_x_kn = 0;
				int i_start_y_kn = 0;

				int i_start_x_kk = 0;
				int i_start_y_kk = 0;

				#region Контроллер и конвейер микропроцессора
				for (int i = 0; i < o_mp.NumberOfController; i++)
				{
					o_graphic.DrawLine(new Pen(Color.Black), 0, step_line_mp, size_width, step_line_mp);
					i_start_y_kn = step_line_mp;

					Rectangle drawRect_k = new Rectangle(0, step_line_mp+ size_step, size_step*2, size_step*2);
					o_graphic.DrawString($"k{i+1}", drawFont, drawBrush, drawRect_k, drawFormat);

					step_line_mp += offset_line;
				}

				o_graphic.DrawLine(new Pen(Color.Black), 0, step_line_mp, size_width, step_line_mp);
				i_start_y_kk = step_line_mp;

				Rectangle drawRect_kk = new Rectangle(0, step_line_mp + size_step, size_step * 2, size_step * 2);
				o_graphic.DrawString("kk", drawFont, drawBrush, drawRect_kk, drawFormat);

				step_line_mp += offset_line;
				#endregion

				#region Команды
				foreach(var o_command in o_mp.CommandList)
				{
					switch (o_command.CommandType)
					{
						case Enums.CommandType.Cache_False:
							{
								var drawRect_command = new Rectangle(i_start_x_kn, i_start_y_kn - size_step, size_step, size_step);
								o_graphic.FillRectangle(new SolidBrush(Color.Chocolate), drawRect_command);
								
								i_start_x_kn += size_step;

								o_graphic.FillRectangle(new SolidBrush(Color.Chocolate), new Rectangle(i_start_x_kn, i_start_y_kn, size_step * o_command.NumberOfClockCycles, size_step));
								i_start_x_kn += (size_step * o_command.NumberOfClockCycles);

								o_graphic.DrawString($"{o_command.Id}", drawFont, drawBrush, drawRect_command, drawFormat);

								break;
							}
					}
				}

				#endregion
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
	}
}
