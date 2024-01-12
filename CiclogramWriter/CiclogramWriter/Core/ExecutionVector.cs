namespace CiclogramWriter.Core
{
	/// <summary>
	/// Вектор выполнения команд. Необходим для корректной отрисовки
	/// последовательности выполнения команд в циклограмме.
	/// </summary>
	public class ExecutionVector
	{
		/// <summary>
		/// Код элемента
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Позиция пикселей по оси Y для отрисовки команд
		/// </summary>
		public int StartPointY { get; set; } = 0;
		/// <summary>
		/// Количество прошедших тактов по оси X
		/// </summary>
		public int NumberOfTact { get; set; } = 0;
		/// <summary>
		///  Флаг - свободен ли вектор для записи
		/// </summary>
		public bool IsFree { get; set; } = true;

		public ExecutionVector() { }

		public ExecutionVector(int id)
		{
			Id = id;
		}
	}
}
