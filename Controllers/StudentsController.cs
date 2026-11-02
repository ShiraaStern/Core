using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService studentService;

    public StudentsController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAll()
    {
        return Ok(studentService.GetAll());
    }

    // GET: api/students/1
    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = studentService.Get(id);
        if (student is null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    // GET: api/students/course/1  ← כל התלמידים לקורס 1
    [HttpGet("course/{courseId}")]
    public ActionResult<IEnumerable<Student>> GetByCourse(int courseId)
    {
        return Ok(studentService.GetByCourse(courseId));
    }

    // POST: api/students
    [HttpPost]
    public ActionResult<Student> Create(Student student)
    {
        studentService.Add(student);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // PUT: api/students/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest();
        }

        var existing = studentService.Get(id);
        if (existing is null)
        {
            return NotFound();
        }

        studentService.Update(student);
        return NoContent();
    }

    // DELETE: api/students/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = studentService.Get(id);
        if (student is null)
        {
            return NotFound();
        }

        studentService.Delete(id);
        return Ok(studentService.Count);
    }
}
