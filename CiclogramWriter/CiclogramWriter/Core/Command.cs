using CiclogramWriter.Enums;
using System;

namespace CiclogramWriter.Core
{
	/// <summary>
	/// Команда микропроцессора
	/// </summary>
	public class Command
	{
		/// <summary>
		/// Код команды (номер)
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Выполнение в кеше
		/// </summary>
		public bool IsCached { get; set; }
		/// <summary>
		/// Выполнение управляющей операции
		/// </summary>
		public bool IsManagementOperation { get; set; }
		/// <summary>
		/// Количество тактов
		/// </summary>
		public int NumberOfClockCycles { get; set; }
		/// <summary>
		/// Приоритет выполнения
		/// </summary>
		public int Priority { get; set; }
		/// <summary>
		/// Статус выполнения
		/// </summary>
		public ExecutionStatus ExecutionStatus { get; set; }
	}
}
