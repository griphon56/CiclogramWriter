namespace CiclogramWriter.Enums
{
	/// <summary>
	/// Статус выполнения
	/// </summary>
	public enum ExecutionStatus
	{
		/// <summary>
		/// Не выполнялась
		/// </summary>
		Inactive,
		/// <summary>
		/// В работе (находится в ожадинии, висит заявка)
		/// </summary>
		InProgress,
		/// <summary>
		/// Выполнена
		/// </summary>
		Completed
	}
}
