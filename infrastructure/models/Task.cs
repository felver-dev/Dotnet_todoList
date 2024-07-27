namespace todo_back.infrastructure.models
{
	public class TaskModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool IsCompleted { get; set; } = false;
		public bool IsDeleted { get; set; } = false;
		public bool IsRead { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; }
	}
}