namespace CiclogramWriter.Enums
{
	/// <summary>
	/// Тип кодманд
	/// </summary>
	public enum CommandType
	{
		/// <summary>
		/// (Кеш; -)
		/// </summary>
		Cache_False,
		/// <summary>
		/// (Кеш; У.О.)
		/// </summary>
		Cache_MO,
		/// <summary>
		/// (Не кеш; -)
		/// </summary>
		NotCache_False,
		/// <summary>
		/// (Не кеш; У.О.)
		/// </summary>
		NotCache_MO
	}
}
