using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_back.Dtos;
using todo_back.infrastructure.context;
using todo_back.infrastructure.models;

namespace todo_back.Controllers
{
	[ApiController]
	[Route("api/tasks")]
	public class TaskController : ControllerBase
	{
		public readonly AppDbContext Context;
		public TaskController(AppDbContext context)
		{
			Context = context;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetTasks()
		{
			var tasks = await Context.Tasks.ToListAsync();
			return Ok(tasks);
		}
		
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetTask(int id)
		{
			if (id == 0) return BadRequest();
			var task = await Context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

			if (task == null) return NotFound();
			return Ok(task); 
		}
		
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreatingTask(TaskDto taskDto)
		{
			var uniqueTask = await Context.Tasks.FirstOrDefaultAsync(u => u.Title.ToLower() == taskDto.Title.ToLower() );
	
			if(uniqueTask != null || taskDto == null)
			{
				ModelState.AddModelError("", "Task already exists or wrong data");
				return BadRequest(ModelState);
			}

			TaskModel task = new()
			{
				Title = taskDto.Title,
				Description = taskDto.Description,
			};
			
			await Context.Tasks.AddAsync(task);
			await Context.SaveChangesAsync();
			return Ok();
		}
		
			
		[HttpPut("delete/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteTask(int id)
		{
			if (id == 0) return BadRequest();
			var task = await Context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
			if (task == null) return NotFound();

			task.IsDeleted = true;
			
			await Context.SaveChangesAsync();
			return Ok(task);
		}
		[HttpPut("restore/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> RestoreTask(int id)
		{
			if (id == 0) return BadRequest();
			var task = await Context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
			if (task == null) return NotFound();

			task.IsDeleted = false;
			
			await Context.SaveChangesAsync();
			return Ok(task);
		}
		[HttpPut("complete/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> CompleteTask(int id)
		{
			if (id == 0) return BadRequest();
			var task = await Context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
			if (task == null) return NotFound();

			task.IsCompleted = true;
			
			await Context.SaveChangesAsync();
			return Ok(task);
		}
		[HttpPut("read/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> ReadTask(int id)
		{
			if (id == 0) return BadRequest();
			var task = await Context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
			if (task == null) return NotFound();

			task.IsRead = true;
			
			await Context.SaveChangesAsync();
			return Ok(task);
		}
		[HttpPut("update/{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateTask(TaskUpdated taskDto, int id)
		{
			if(taskDto == null || id == 0) return BadRequest();

			var task = await Context.Tasks.FindAsync(id);
			if (task == null) return NotFound();

			task.Title = taskDto.Title;
			task.Description = taskDto.Description;
			task.UpdatedAt = DateTime.Now;
			
			await Context.SaveChangesAsync();
			return Ok(task);
		}
	}
}