using System.Collections.Generic;

namespace CiclogramWriter.Core
{
	/// <summary>
	/// Микропроцессор
	/// </summary>
	public class Microprocessor
	{
		/// <summary>
		/// Код МП (номер)
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Список команд
		/// </summary>
		public List<Command> CommandList { get; set; } = new List<Command>();
		/// <summary>
		/// Список заявок
		/// </summary>
		public List<Request> RequestList { get; set; } = new List<Request>();
		/// <summary>
		/// Вектор отображения контроллера
		/// </summary>
		public ExecutionVector Controller { get; set; } = new ExecutionVector();
		/// <summary>
		/// Вектор отображения конвейера
		/// </summary>
		public ExecutionVector Conveyor { get; set; } = new ExecutionVector();
		/// <summary>
		/// Количество контроллеров
		/// </summary>
		public int NumberOfController { get; set; } = 1;
		/// <summary>
		/// Количество конвейеров
		/// </summary>
		public int NumberOfConveyors { get; private set; } = 1;
	}
}
