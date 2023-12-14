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
		/// f(мп) = 2f(сш)
		/// Отрисовка в К1 - контроллер (верхняя линия).
		/// Если команда 2 такта - тогда на графике отрисовывается 4 такта для системной шины (СШ), закрашеные квадраты.
		/// и 2 такта для микропроцессора (мп), не закрашенные (отрисованы снизу).
		/// </summary>
		public int Fsh { get; set; } = 2;
		/// <summary>
		/// F(оп)
		/// Отрисовака в КК - конвеер (нижняя линия).
		/// В отрисовка 2 квадрата * F(оп)
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
