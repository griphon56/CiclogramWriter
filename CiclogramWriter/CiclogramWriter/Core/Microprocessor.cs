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
		public List<Command> CommandList { get; set; }
		/// <summary>
		/// Список заявок
		/// </summary>
		public List<Request> RequestList { get; set; }
		/// <summary>
		/// Прямой доступ к памяти (англ. direct memory access, DMA)
		/// </summary>
		public bool NeedDMA { get; set; } = false;
	}
}
