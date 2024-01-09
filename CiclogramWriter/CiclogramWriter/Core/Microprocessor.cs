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
		/// Список векторов отображения контроллеров
		/// </summary>
		public List<ExecutionVector> ControllerList { get; set; } = new List<ExecutionVector>();
		/// <summary>
		/// Вектор отображения конвейера
		/// </summary>
		public ExecutionVector Conveyor { get; set; } = new ExecutionVector();
		/// <summary>
		/// Вектор отображения DMA.
		/// Прямой доступ к памяти — режим обмена данными между устройствами компьютера или же между устройством и основной памятью,
		/// в котором центральный процессор не участвует. Так как данные не пересылаются в ЦП и обратно, скорость передачи увеличивается.
		/// </summary>
		public ExecutionVector DirectMemoryAccess { get; set; }
		/// <summary>
		/// Счетчик тактов (необходим для получения информации, как долго будет занята системная шина)
		/// </summary>
		public int NumberOfTactTime { get; set; } = 0;
		/// <summary>
		/// Счетчик
		/// </summary>
		public int NumberOfСount { get; set; } = 0;

		public Microprocessor() { }

		public Microprocessor(int id)
		{
			Id = id;
		}
	}
}
