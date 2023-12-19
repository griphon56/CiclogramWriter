using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiclogramWriter.Enums
{
	/// <summary>
	/// Состояние выполнения команды
	/// </summary>
	public enum StateCommand
	{
		/// <summary>
		/// Выбираем, декодируем
		/// </summary>
		Decode,
		/// <summary>
		/// Шина микропроцессора
		/// </summary>
		MicroBus,
		/// <summary>
		/// Системная шина контроллера
		/// </summary>
		SystemBusKN,
		/// <summary>
		/// Системная шина конвейера
		/// </summary>
		SystemBusKK
	}
}
