using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPro.Data;
using StudyPro.Models;

namespace StudyPro.Controllers;

[Route("studytasks")]
[ApiController]
public class StudyTaskController : Controller
{
    private readonly StudyProContext _db;

    public StudyTaskController(StudyProContext db)
    {
        _db = db;
    }

    // GET /studytasks
    [HttpGet]
    public async Task<ActionResult<List<StudyTask>>> GetStudyTasks()
    {
        return await _db.StudyTasks
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    // GET /studytasks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudyTask>> GetStudyTask(int id)
    {
        var task = await _db.StudyTasks.FindAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    // POST /studytasks
    [HttpPost]
    public async Task<ActionResult<StudyTask>> CreateStudyTask(StudyTask task)
    {
        task.Id = 0;
        task.CreatedAt = DateTime.UtcNow;

        _db.StudyTasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudyTask), new { id = task.Id }, task);
    }

    // PUT /studytasks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudyTask(int id, StudyTask updatedTask)
    {
        if (id != updatedTask.Id)
        {
            return BadRequest();
        }

        var existingTask = await _db.StudyTasks.FindAsync(id);
        if (existingTask is null)
        {
            return NotFound();
        }

        existingTask.Title = updatedTask.Title;
        existingTask.Subject = updatedTask.Subject;
        existingTask.Notes = updatedTask.Notes;
        existingTask.DueDate = updatedTask.DueDate;
        existingTask.IsCompleted = updatedTask.IsCompleted;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /studytasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudyTask(int id)
    {
        var task = await _db.StudyTasks.FindAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        _db.StudyTasks.Remove(task);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}