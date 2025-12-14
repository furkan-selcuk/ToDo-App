using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ToDo.Domain.Enums;

namespace ToDo.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var tasks = await _taskService.GetAllAsync(userId);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var task = await _taskService.GetByIdAsync(id, userId);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter(
        [FromQuery] TaskState? state,
        [FromQuery] int? categoryId)
    {
        var userId = GetUserId();
        var tasks = await _taskService.GetFilteredAsync(
            userId,
            state,
            categoryId
        );

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        task.UserId = GetUserId();
        await _taskService.AddAsync(task);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem task)
    {
        if (id != task.Id)
            return BadRequest();

        await _taskService.UpdateAsync(task, GetUserId());
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteAsync(id, GetUserId());
        return NoContent();
    }
}
