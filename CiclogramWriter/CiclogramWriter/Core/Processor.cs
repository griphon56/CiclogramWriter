using System.Collections.Generic;

namespace CiclogramWriter.Core
{
	/// <summary>
	/// Процессор
	/// </summary>
	public class Processor
	{
		/// <summary>
		/// f(мп) > f(сш)
		/// </summary>
		public int Fsh { get; set; } = 2;
		/// <summary>
		/// F(оп)
		/// </summary>
		public int Fop { get; set; } = 3;
		/// <summary>
		/// Вероятность добавления в кэш (необходимо для рандомной генерации команд)
		/// </summary>
		public int ProbabilityAddToCache { get; set; } = 75;
		/// <summary>
		/// Список микропроцессоров
		/// </summary>
		public List<Microprocessor> MPList { get; set; } = new List<Microprocessor>();
	}
}
