namespace CiclogramWriter.Enums
{
	/// <summary>
	/// Тип кодманд
	/// </summary>
	public enum CommandType
	{
		/// <summary>
		/// (Кеш; -)
		/// (Кеш; Вычислительное)
		/// </summary>
		Cache_False,
		/// <summary>
		/// (Кеш; У.О.)
		/// (Кеш; Управляющая)
		/// </summary>
		Cache_MO,
		/// <summary>
		/// (Не кеш; -)
		/// (Не кеш; Вычислительное)
		/// </summary>
		NotCache_False,
		/// <summary>
		/// (Не кеш; У.О.)
		/// (Не кеш; Управляющая)
		/// </summary>
		NotCache_MO,
		/// <summary>
		/// ДМА
		/// </summary>
		DMA
	}
}
