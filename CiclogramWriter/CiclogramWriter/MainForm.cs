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
				Id = id_mp
			};

			tb_num_pm.Text = id_mp.ToString();
			processor.MPList.Add(o_mp_new);
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
					MessageBox.Show($"Микропроцессор {id_mp} успешно удален.", "Информация");
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

			o_mp.CommandList.Add(o_command);
		}
	}
}
