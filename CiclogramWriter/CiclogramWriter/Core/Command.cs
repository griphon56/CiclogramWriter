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
		/// Количество тактов
		/// </summary>
		public int NumberOfClockCycles { get; set; }
		/// <summary>
		/// Тип команды
		/// </summary>
		public CommandType CommandType { get; set; }
	}
}
