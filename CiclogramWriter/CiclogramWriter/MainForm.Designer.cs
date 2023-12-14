
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
			this.gb_list_command = new System.Windows.Forms.GroupBox();
			this.rt_list_command = new System.Windows.Forms.RichTextBox();
			this.gb_settings = new System.Windows.Forms.GroupBox();
			this.tb_ver_in_cache = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tb_f_op = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tb_mp_sh = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.gb_command_set = new System.Windows.Forms.GroupBox();
			this.num_count_tact = new System.Windows.Forms.NumericUpDown();
			this.btn_add_command = new System.Windows.Forms.Button();
			this.cb_yo = new System.Windows.Forms.CheckBox();
			this.cb_in_cache = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gb_pm = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tb_num_pm = new System.Windows.Forms.TextBox();
			this.btn_del_pm = new System.Windows.Forms.Button();
			this.btn_add_pm = new System.Windows.Forms.Button();
			this.p_canvas = new System.Windows.Forms.Panel();
			this.pb_canvas = new System.Windows.Forms.PictureBox();
			this.sidebar_panel.SuspendLayout();
			this.settings_panel.SuspendLayout();
			this.gb_list_command.SuspendLayout();
			this.gb_settings.SuspendLayout();
			this.gb_command_set.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_count_tact)).BeginInit();
			this.gb_pm.SuspendLayout();
			this.p_canvas.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pb_canvas)).BeginInit();
			this.SuspendLayout();
			// 
			// sidebar_panel
			// 
			this.sidebar_panel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.sidebar_panel.Controls.Add(this.settings_panel);
			this.sidebar_panel.Location = new System.Drawing.Point(0, 0);
			this.sidebar_panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.sidebar_panel.Name = "sidebar_panel";
			this.sidebar_panel.Size = new System.Drawing.Size(293, 626);
			this.sidebar_panel.TabIndex = 0;
			// 
			// settings_panel
			// 
			this.settings_panel.BackColor = System.Drawing.SystemColors.Window;
			this.settings_panel.Controls.Add(this.gb_list_command);
			this.settings_panel.Controls.Add(this.gb_settings);
			this.settings_panel.Controls.Add(this.gb_command_set);
			this.settings_panel.Controls.Add(this.gb_pm);
			this.settings_panel.Location = new System.Drawing.Point(10, 9);
			this.settings_panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.settings_panel.Name = "settings_panel";
			this.settings_panel.Size = new System.Drawing.Size(272, 604);
			this.settings_panel.TabIndex = 0;
			// 
			// gb_list_command
			// 
			this.gb_list_command.Controls.Add(this.rt_list_command);
			this.gb_list_command.Location = new System.Drawing.Point(4, 320);
			this.gb_list_command.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_list_command.Name = "gb_list_command";
			this.gb_list_command.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_list_command.Size = new System.Drawing.Size(262, 274);
			this.gb_list_command.TabIndex = 3;
			this.gb_list_command.TabStop = false;
			this.gb_list_command.Text = "Список команд";
			// 
			// rt_list_command
			// 
			this.rt_list_command.Location = new System.Drawing.Point(5, 20);
			this.rt_list_command.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.rt_list_command.Name = "rt_list_command";
			this.rt_list_command.Size = new System.Drawing.Size(252, 264);
			this.rt_list_command.TabIndex = 0;
			this.rt_list_command.Text = "";
			// 
			// gb_settings
			// 
			this.gb_settings.Controls.Add(this.tb_ver_in_cache);
			this.gb_settings.Controls.Add(this.label5);
			this.gb_settings.Controls.Add(this.tb_f_op);
			this.gb_settings.Controls.Add(this.label4);
			this.gb_settings.Controls.Add(this.tb_mp_sh);
			this.gb_settings.Controls.Add(this.label3);
			this.gb_settings.Location = new System.Drawing.Point(4, 201);
			this.gb_settings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_settings.Name = "gb_settings";
			this.gb_settings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_settings.Size = new System.Drawing.Size(262, 115);
			this.gb_settings.TabIndex = 2;
			this.gb_settings.TabStop = false;
			this.gb_settings.Text = "Настройка";
			// 
			// tb_ver_in_cache
			// 
			this.tb_ver_in_cache.Location = new System.Drawing.Point(5, 87);
			this.tb_ver_in_cache.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tb_ver_in_cache.Name = "tb_ver_in_cache";
			this.tb_ver_in_cache.Size = new System.Drawing.Size(253, 23);
			this.tb_ver_in_cache.TabIndex = 5;
			this.tb_ver_in_cache.TextChanged += new System.EventHandler(this.tb_ver_in_cache_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(173, 15);
			this.label5.TabIndex = 4;
			this.label5.Text = "Вероятность попадания в кеш";
			// 
			// tb_f_op
			// 
			this.tb_f_op.Location = new System.Drawing.Point(135, 40);
			this.tb_f_op.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tb_f_op.Name = "tb_f_op";
			this.tb_f_op.Size = new System.Drawing.Size(123, 23);
			this.tb_f_op.TabIndex = 3;
			this.tb_f_op.TextChanged += new System.EventHandler(this.tb_f_op_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(135, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 15);
			this.label4.TabIndex = 2;
			this.label4.Text = "F(ОП)";
			// 
			// tb_mp_sh
			// 
			this.tb_mp_sh.Location = new System.Drawing.Point(5, 40);
			this.tb_mp_sh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tb_mp_sh.Name = "tb_mp_sh";
			this.tb_mp_sh.Size = new System.Drawing.Size(123, 23);
			this.tb_mp_sh.TabIndex = 1;
			this.tb_mp_sh.TextChanged += new System.EventHandler(this.tb_mp_sh_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "f(МП) > f(СШ)";
			// 
			// gb_command_set
			// 
			this.gb_command_set.Controls.Add(this.num_count_tact);
			this.gb_command_set.Controls.Add(this.btn_add_command);
			this.gb_command_set.Controls.Add(this.cb_yo);
			this.gb_command_set.Controls.Add(this.cb_in_cache);
			this.gb_command_set.Controls.Add(this.label2);
			this.gb_command_set.Location = new System.Drawing.Point(4, 76);
			this.gb_command_set.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_command_set.Name = "gb_command_set";
			this.gb_command_set.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_command_set.Size = new System.Drawing.Size(262, 121);
			this.gb_command_set.TabIndex = 1;
			this.gb_command_set.TabStop = false;
			this.gb_command_set.Text = "Настройка команд";
			// 
			// num_count_tact
			// 
			this.num_count_tact.Location = new System.Drawing.Point(5, 36);
			this.num_count_tact.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_count_tact.Name = "num_count_tact";
			this.num_count_tact.Size = new System.Drawing.Size(252, 23);
			this.num_count_tact.TabIndex = 6;
			this.num_count_tact.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btn_add_command
			// 
			this.btn_add_command.Location = new System.Drawing.Point(5, 88);
			this.btn_add_command.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btn_add_command.Name = "btn_add_command";
			this.btn_add_command.Size = new System.Drawing.Size(252, 22);
			this.btn_add_command.TabIndex = 6;
			this.btn_add_command.Text = "Добавить команду";
			this.btn_add_command.UseVisualStyleBackColor = true;
			this.btn_add_command.Click += new System.EventHandler(this.btn_add_command_Click);
			// 
			// cb_yo
			// 
			this.cb_yo.AutoSize = true;
			this.cb_yo.Location = new System.Drawing.Point(133, 65);
			this.cb_yo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cb_yo.Name = "cb_yo";
			this.cb_yo.Size = new System.Drawing.Size(93, 19);
			this.cb_yo.TabIndex = 5;
			this.cb_yo.Text = "Команда УО";
			this.cb_yo.UseVisualStyleBackColor = true;
			// 
			// cb_in_cache
			// 
			this.cb_in_cache.AutoSize = true;
			this.cb_in_cache.Location = new System.Drawing.Point(5, 65);
			this.cb_in_cache.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cb_in_cache.Name = "cb_in_cache";
			this.cb_in_cache.Size = new System.Drawing.Size(115, 19);
			this.cb_in_cache.TabIndex = 4;
			this.cb_in_cache.Text = "Команда в кеше";
			this.cb_in_cache.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Кол-во тактов";
			// 
			// gb_pm
			// 
			this.gb_pm.Controls.Add(this.label1);
			this.gb_pm.Controls.Add(this.tb_num_pm);
			this.gb_pm.Controls.Add(this.btn_del_pm);
			this.gb_pm.Controls.Add(this.btn_add_pm);
			this.gb_pm.Location = new System.Drawing.Point(4, 4);
			this.gb_pm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_pm.Name = "gb_pm";
			this.gb_pm.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gb_pm.Size = new System.Drawing.Size(262, 68);
			this.gb_pm.TabIndex = 0;
			this.gb_pm.TabStop = false;
			this.gb_pm.Text = "Микропроцессор";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "№ МП";
			// 
			// tb_num_pm
			// 
			this.tb_num_pm.Location = new System.Drawing.Point(5, 34);
			this.tb_num_pm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tb_num_pm.Name = "tb_num_pm";
			this.tb_num_pm.Size = new System.Drawing.Size(84, 23);
			this.tb_num_pm.TabIndex = 3;
			// 
			// btn_del_pm
			// 
			this.btn_del_pm.Location = new System.Drawing.Point(177, 34);
			this.btn_del_pm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btn_del_pm.Name = "btn_del_pm";
			this.btn_del_pm.Size = new System.Drawing.Size(81, 22);
			this.btn_del_pm.TabIndex = 1;
			this.btn_del_pm.Text = "Удалить";
			this.btn_del_pm.UseVisualStyleBackColor = true;
			this.btn_del_pm.Click += new System.EventHandler(this.btn_del_pm_Click);
			// 
			// btn_add_pm
			// 
			this.btn_add_pm.Location = new System.Drawing.Point(96, 34);
			this.btn_add_pm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btn_add_pm.Name = "btn_add_pm";
			this.btn_add_pm.Size = new System.Drawing.Size(76, 22);
			this.btn_add_pm.TabIndex = 0;
			this.btn_add_pm.Text = "Добавить";
			this.btn_add_pm.UseVisualStyleBackColor = true;
			this.btn_add_pm.Click += new System.EventHandler(this.btn_add_pm_Click);
			// 
			// p_canvas
			// 
			this.p_canvas.BackColor = System.Drawing.SystemColors.Info;
			this.p_canvas.Controls.Add(this.pb_canvas);
			this.p_canvas.Location = new System.Drawing.Point(294, 0);
			this.p_canvas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.p_canvas.Name = "p_canvas";
			this.p_canvas.Size = new System.Drawing.Size(823, 626);
			this.p_canvas.TabIndex = 1;
			// 
			// pb_canvas
			// 
			this.pb_canvas.Location = new System.Drawing.Point(0, 0);
			this.pb_canvas.Name = "pb_canvas";
			this.pb_canvas.Size = new System.Drawing.Size(657, 383);
			this.pb_canvas.TabIndex = 0;
			this.pb_canvas.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1114, 622);
			this.Controls.Add(this.p_canvas);
			this.Controls.Add(this.sidebar_panel);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "MainForm";
			this.Text = "CiclogramWriter";
			this.sidebar_panel.ResumeLayout(false);
			this.settings_panel.ResumeLayout(false);
			this.gb_list_command.ResumeLayout(false);
			this.gb_settings.ResumeLayout(false);
			this.gb_settings.PerformLayout();
			this.gb_command_set.ResumeLayout(false);
			this.gb_command_set.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_count_tact)).EndInit();
			this.gb_pm.ResumeLayout(false);
			this.gb_pm.PerformLayout();
			this.p_canvas.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pb_canvas)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel sidebar_panel;
		private System.Windows.Forms.Panel settings_panel;
		private System.Windows.Forms.GroupBox gb_pm;
		private System.Windows.Forms.Button btn_del_pm;
		private System.Windows.Forms.Button btn_add_pm;
		private System.Windows.Forms.GroupBox gb_command_set;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox gb_settings;
		private System.Windows.Forms.Button btn_add_command;
		private System.Windows.Forms.CheckBox cb_yo;
		private System.Windows.Forms.CheckBox cb_in_cache;
		private System.Windows.Forms.TextBox tb_ver_in_cache;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tb_f_op;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tb_mp_sh;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox gb_list_command;
		private System.Windows.Forms.RichTextBox rt_list_command;
		private System.Windows.Forms.Panel p_canvas;
		private System.Windows.Forms.NumericUpDown num_count_tact;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tb_num_pm;
		private System.Windows.Forms.PictureBox pb_canvas;
	}
}

