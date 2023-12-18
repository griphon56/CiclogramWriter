using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public int WidthCanvas { get; private set; } = 950;
		/// <summary>
		/// Высота холста
		/// </summary>
		public int HeightCanvas { get; private set; } = 850;
		/// <summary>
		/// Отступ между основными линиями (Контроллер, конвейер)
		/// </summary>
		public int IndentLine { get; private set; } = SquareSize * 5;
		/// <summary>
		/// Шаг отступа между линими (Контроллер, конвейер)
		/// </summary>
		public int StepIndentLine { get; set; } = SquareSize * 5;

		private Font DrawFont { get; set; } = new Font("Calibri", 10);
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
			for (int i = 0; i < 100; i++)
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
		/// <param name="end_point_x">Возвращает конечную точку по Х</param>
		public void DrawCacheKN(Graphics o_graphic, string s_content, int point_x, int point_y, out int end_point_x)
		{
			var drawRect_command = new Rectangle(point_x, point_y - DrawChart.SquareSize, DrawChart.SquareSize, DrawChart.SquareSize);
			o_graphic.FillRectangle(new SolidBrush(Color.Chocolate), drawRect_command);

			o_graphic.DrawString(s_content, DrawFont, DrawBrush, drawRect_command, DrawFormat);

			end_point_x = DrawChart.SquareSize;
		}
		/// <summary>
		/// Метод отрисовки шины микропроцессора в контроллере
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="num_cycles">Количество тактов</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		/// <param name="end_point_x">Возвращает конечную точку по Х</param>
		public void DrawMicroBusKN(Graphics o_graphic, int num_cycles, int point_x, int point_y, out int end_point_x)
		{
			o_graphic.FillRectangle(new SolidBrush(Color.Chocolate), new Rectangle(point_x, point_y, DrawChart.SquareSize * num_cycles, DrawChart.SquareSize));

			end_point_x = (DrawChart.SquareSize * num_cycles);
		}

		/// <summary>
		/// Метод отрисовки метод системной шины в контроллер
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="num_cycles">Количество тактов</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		/// <param name="end_point_x">Возвращает конечную точку по Х</param>
		public void DrawSystemBusKN(Graphics o_graphic, int num_cycles, int point_x, int point_y, out int end_point_x)
		{
			o_graphic.FillRectangle(new SolidBrush(Color.Chocolate), new Rectangle(point_x, point_y, DrawChart.SquareSize * num_cycles, DrawChart.SquareSize));

			end_point_x = (DrawChart.SquareSize * num_cycles);
		}
		/// <summary>
		/// Метод отрисовки системной шины в конвейере.
		/// </summary>
		/// <param name="o_graphic"></param>
		/// <param name="num_cycles">Количество тактов</param>
		/// <param name="point_x">Начальная точка Х </param>
		/// <param name="point_y">Начальная точка У</param>
		/// <param name="end_point_x">Возвращает конечную точку по Х</param>
		public void DrawSystemBusKK(Graphics o_graphic, int num_cycles, int point_x, int point_y, out int end_point_x)
		{
			o_graphic.FillRectangle(new SolidBrush(Color.Chocolate), new Rectangle(point_x, point_y, DrawChart.SquareSize * num_cycles, DrawChart.SquareSize));

			end_point_x = (DrawChart.SquareSize * num_cycles);
		}
	}
}
