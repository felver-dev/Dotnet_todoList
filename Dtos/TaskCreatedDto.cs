using System.ComponentModel.DataAnnotations;

namespace todo_back.Dtos
{
	public class TaskDto
	{
		[Required, MaxLength(255)]
		public string Title { get; set; } = string.Empty;
		 [Required, MaxLength(255)]
		public string Description { get; set; } = string.Empty;

	}
	
	public class TaskUpdated
	{
		[Required, MaxLength(255)]
		public string Title { get; set; } = string.Empty;
		 [Required, MaxLength(255)]
		public string Description { get; set; } = string.Empty;
		public DateTime UpdatedAt { get; set; }
	}
}