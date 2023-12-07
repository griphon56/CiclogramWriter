
namespace CiclogramWriter
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.sidebar_panel = new System.Windows.Forms.Panel();
			this.settings_panel = new System.Windows.Forms.Panel();
			this.gb_pm = new System.Windows.Forms.GroupBox();
			this.btn_del_pm = new System.Windows.Forms.Button();
			this.btn_add_pm = new System.Windows.Forms.Button();
			this.gb_command_set = new System.Windows.Forms.GroupBox();
			this.tb_num_pm = new System.Windows.Forms.TextBox();
			this.tb_count_tact = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.sidebar_panel.SuspendLayout();
			this.settings_panel.SuspendLayout();
			this.gb_pm.SuspendLayout();
			this.gb_command_set.SuspendLayout();
			this.SuspendLayout();
			// 
			// sidebar_panel
			// 
			this.sidebar_panel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.sidebar_panel.Controls.Add(this.settings_panel);
			this.sidebar_panel.Location = new System.Drawing.Point(0, 0);
			this.sidebar_panel.Name = "sidebar_panel";
			this.sidebar_panel.Size = new System.Drawing.Size(335, 709);
			this.sidebar_panel.TabIndex = 0;
			// 
			// settings_panel
			// 
			this.settings_panel.BackColor = System.Drawing.SystemColors.Window;
			this.settings_panel.Controls.Add(this.gb_command_set);
			this.settings_panel.Controls.Add(this.gb_pm);
			this.settings_panel.Location = new System.Drawing.Point(12, 12);
			this.settings_panel.Name = "settings_panel";
			this.settings_panel.Size = new System.Drawing.Size(311, 687);
			this.settings_panel.TabIndex = 0;
			// 
			// gb_pm
			// 
			this.gb_pm.Controls.Add(this.btn_del_pm);
			this.gb_pm.Controls.Add(this.btn_add_pm);
			this.gb_pm.Location = new System.Drawing.Point(5, 5);
			this.gb_pm.Name = "gb_pm";
			this.gb_pm.Size = new System.Drawing.Size(300, 74);
			this.gb_pm.TabIndex = 0;
			this.gb_pm.TabStop = false;
			this.gb_pm.Text = "Микропроцессор";
			// 
			// btn_del_pm
			// 
			this.btn_del_pm.Location = new System.Drawing.Point(152, 26);
			this.btn_del_pm.Name = "btn_del_pm";
			this.btn_del_pm.Size = new System.Drawing.Size(142, 30);
			this.btn_del_pm.TabIndex = 1;
			this.btn_del_pm.Text = "Удалить";
			this.btn_del_pm.UseVisualStyleBackColor = true;
			// 
			// btn_add_pm
			// 
			this.btn_add_pm.Location = new System.Drawing.Point(6, 26);
			this.btn_add_pm.Name = "btn_add_pm";
			this.btn_add_pm.Size = new System.Drawing.Size(140, 30);
			this.btn_add_pm.TabIndex = 0;
			this.btn_add_pm.Text = "Добавить";
			this.btn_add_pm.UseVisualStyleBackColor = true;
			// 
			// gb_command_set
			// 
			this.gb_command_set.Controls.Add(this.label2);
			this.gb_command_set.Controls.Add(this.label1);
			this.gb_command_set.Controls.Add(this.tb_count_tact);
			this.gb_command_set.Controls.Add(this.tb_num_pm);
			this.gb_command_set.Location = new System.Drawing.Point(5, 85);
			this.gb_command_set.Name = "gb_command_set";
			this.gb_command_set.Size = new System.Drawing.Size(300, 314);
			this.gb_command_set.TabIndex = 1;
			this.gb_command_set.TabStop = false;
			this.gb_command_set.Text = "Настройка команд";
			// 
			// tb_num_pm
			// 
			this.tb_num_pm.Location = new System.Drawing.Point(6, 50);
			this.tb_num_pm.Name = "tb_num_pm";
			this.tb_num_pm.Size = new System.Drawing.Size(140, 27);
			this.tb_num_pm.TabIndex = 0;
			// 
			// tb_count_tact
			// 
			this.tb_count_tact.Location = new System.Drawing.Point(152, 50);
			this.tb_count_tact.Name = "tb_count_tact";
			this.tb_count_tact.Size = new System.Drawing.Size(142, 27);
			this.tb_count_tact.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "№ МП";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(152, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Кол-во тактов";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1273, 711);
			this.Controls.Add(this.sidebar_panel);
			this.Name = "MainForm";
			this.Text = "CiclogramWriter";
			this.sidebar_panel.ResumeLayout(false);
			this.settings_panel.ResumeLayout(false);
			this.gb_pm.ResumeLayout(false);
			this.gb_command_set.ResumeLayout(false);
			this.gb_command_set.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel sidebar_panel;
		private System.Windows.Forms.Panel settings_panel;
		private System.Windows.Forms.GroupBox gb_pm;
		private System.Windows.Forms.Button btn_del_pm;
		private System.Windows.Forms.Button btn_add_pm;
		private System.Windows.Forms.GroupBox gb_command_set;
		private System.Windows.Forms.TextBox tb_count_tact;
		private System.Windows.Forms.TextBox tb_num_pm;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}

