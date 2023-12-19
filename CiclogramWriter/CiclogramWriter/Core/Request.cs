using CiclogramWriter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiclogramWriter.Core
{
	/// <summary>
	/// Заявка
	/// </summary>
	public class Request
	{
		/// <summary>
		/// Команда
		/// </summary>
		public Command Command { get; set; }
		/// <summary>
		/// Приоритет выполнения
		/// </summary>
		public int Priority { get; set; }
		/// <summary>
		/// Состояние выполнения команды
		/// </summary>
		public StateCommand StateCommand { get; set; }
	}
}
