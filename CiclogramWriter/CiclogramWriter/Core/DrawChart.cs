using System.Drawing;

namespace CiclogramWriter.Core
{
	/// <summary>
	/// Класс отрисовки графика.
	/// </summary>
	public class DrawChart
	{
		/// <summary>
		/// Размер сетки квадрата (шаг)
		/// </summary>
		public static int SquareSize { get; private set; } = 20;
		/// <summary>
		/// Ширина холста
		/// </summary>
		public int WidthCanvas { get; private set; } = 10000;
		/// <summary>
		/// Высота холста
		/// </summary>
		public int HeightCanvas { get; private set; } = 814;
		/// <summary>
		/// Отступ между основными линиями (Контроллер, конвейер)
		/// </summary>
		public int IndentLine { get; private set; } = SquareSize * 5;
		/// <summary>
		/// Шаг отступа между линими (Контроллер, конвейер)
		/// </summary>
		public int StepIndentLine { get; set; } = SquareSize * 5;

		private Font DrawFont { get; set; } = new Font("Calibri", 8);
		private SolidBrush DrawBrush { get; set; } = new SolidBrush(Color.Black);
		private StringFormat DrawFormat { get; set; }  = new StringFormat();

		/// <summary>
		/// Метод отрисовки сетки
		/// </summary>
		/// <param name="o_graphic"></param>
		public void DrawGrid(Graphics o_graphic)
		{
			// Вертикальные линии
			int step = 0;
			for (int i = 0; i < 1000; i++)
			{
				o_graphic.DrawLine(new Pen(Color.Silver), step, 0, step, this.HeightCanvas);
				step += SquareSize;
			}

			// Горизонтальные линии
			step = 0;
			for (int i = 0; i < 100; i++)
			{
				o_graphic.DrawLine(new Pen(Color.Silver), 0, step, this.WidthCanvas, step);
				step += SquareSize;
			}
		}

		/// <summary>
		/// Метод отрисовки одной линии (контроллер, конвейер)
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="s_content">Подпись линии</param>
		/// <param name="start_point_y">Возвращает начальное значение отрисовки линии по оси Y </param>
		public void DrawLine(Graphics o_graphic, string s_content, out int start_point_y)
		{
			o_graphic.DrawLine(new Pen(Color.Black), 0, this.StepIndentLine, this.WidthCanvas, this.StepIndentLine);
			start_point_y = this.StepIndentLine;

			Rectangle drawRect_k = new Rectangle(0, this.StepIndentLine + SquareSize, SquareSize * 2, SquareSize * 2);
			o_graphic.DrawString(s_content, DrawFont, DrawBrush, drawRect_k, DrawFormat);
		}
		/// <summary>
		/// Метод отрисовки кеша (выбираем, декодируем)
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		public void DrawCacheKN(Graphics o_graphic, string s_content, int point_x, int point_y)
		{
			var drawRect_command = new Rectangle(point_x, point_y - DrawChart.SquareSize, DrawChart.SquareSize, DrawChart.SquareSize);
			o_graphic.FillRectangle(new SolidBrush(Color.LightGreen), drawRect_command);

			o_graphic.DrawString(s_content, DrawFont, DrawBrush, drawRect_command, DrawFormat);
		}
		/// <summary>
		/// Метод отрисовки шины микропроцессора в контроллере
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="num_cycles">Количество тактов</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		public void DrawMicroBusKN(Graphics o_graphic, int num_cycles, int point_x, int point_y)
		{
			int i_width = DrawChart.SquareSize * num_cycles;
			o_graphic.FillRectangle(new SolidBrush(Color.LightGreen), new Rectangle(point_x, point_y, i_width, DrawChart.SquareSize));
		}

		/// <summary>
		/// Метод отрисовки метод системной шины в контроллер
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="num_cycles">Количество тактов</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		/// <param name="f_sh">f(сш)</param>
		public void DrawSystemBusKN(Graphics o_graphic, string s_content, int num_cycles, int point_x, int point_y, int f_sh)
		{
			int i_width = DrawChart.SquareSize * num_cycles * f_sh;
			var drawRect_command = new Rectangle(point_x, point_y, i_width, DrawChart.SquareSize);
			o_graphic.FillRectangle(new SolidBrush(Color.LightSkyBlue), drawRect_command);

			o_graphic.DrawString(s_content, DrawFont, DrawBrush, drawRect_command, DrawFormat);
		}
		/// <summary>
		/// Метод отрисовки системной шины в конвейере.
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="num_cycles">Количество тактов</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		/// <param name="f_op">F(оп)</param>
		public void DrawSystemBusKK(Graphics o_graphic, string s_content, int num_cycles, int point_x, int point_y, int f_op)
		{
			int i_width = DrawChart.SquareSize * 2 * num_cycles * f_op;

			var drawRect_command = new Rectangle(point_x, point_y, i_width, DrawChart.SquareSize);
			o_graphic.FillRectangle(new SolidBrush(Color.LightSkyBlue), drawRect_command);

			o_graphic.DrawString(s_content, DrawFont, DrawBrush, drawRect_command, DrawFormat);
		}
		/// <summary>
		/// Метод отрисовки заявки
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="s_content">Номер заявки</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		public void DrawRequest(Graphics o_graphic, string s_content, int point_x, int point_y)
		{
			var drawRect_command = new Rectangle(point_x, point_y - DrawChart.SquareSize * 2, DrawChart.SquareSize, DrawChart.SquareSize);
			o_graphic.DrawString(s_content, DrawFont, DrawBrush, drawRect_command, DrawFormat);

			o_graphic.DrawLine(new Pen(Color.Black), point_x, point_y - DrawChart.SquareSize, point_x, point_y - DrawChart.SquareSize * 2);
		}
	}
}
