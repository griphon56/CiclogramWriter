namespace CiclogramWriter.Core
{
	/// <summary>
	/// Модель прямого доступа к памяти (DMA)
	/// </summary>
	public class DirectMemoryAccess : ExecutionVector
	{
		public DirectMemoryAccess() { }

		public DirectMemoryAccess(int id) : base(id) { }
	}
}
