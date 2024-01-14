namespace CiclogramWriter.Core
{
	/// <summary>
	/// Модель контроллера
	/// </summary>
	public class Controller : ExecutionVector
	{
		/// <summary>
		/// Позиция пикселей по оси X для отрисовки заявок
		/// </summary>
		public int StartRequestPointX { get; set; } = 0;
		/// <summary>
		/// Позиция пикселей по оси Y для отрисовки заявок
		/// </summary>
		public int StartRequestPointY { get; set; } = 0;

		public Controller() { }

		public Controller(int id) : base(id) { }
	}
}
