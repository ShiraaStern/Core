using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService courseService;

    public CoursesController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    // GET: api/courses
    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAll()
    {
        return Ok(courseService.GetAll());
    }

    // GET: api/courses/1
    [HttpGet("{id}")]
    public ActionResult<Course> GetById(int id)
    {
        var course = courseService.Get(id);
        if (course is null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    // POST: api/courses
    [HttpPost]
    public ActionResult<Course> Create(Course course)
    {
        courseService.Add(course);
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    // PUT: api/courses/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, Course course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        var existing = courseService.Get(id);
        if (existing is null)
        {
            return NotFound();
        }

        courseService.Update(course);
        return NoContent();
    }

    // DELETE: api/courses/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var course = courseService.Get(id);
        if (course is null)
        {
            return NotFound();
        }

        courseService.Delete(id);
        return Ok(courseService.Count);
    }
}
