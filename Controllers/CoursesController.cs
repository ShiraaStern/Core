using Microsoft.AspNetCore.Mvc;

namespace core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    // רשימת קורסים לדוגמה בזיכרון (בהמשך אפשר להחליף בבסיס נתונים)
    private static readonly List<Course> Courses = new()
    {
        new Course
        {
            Id = 1,
            Name = "שם קורס 1",
            Description = "טקסט לדוגמה",
            Credits = 3,
            TeacherName = "שם מלא 1"
        },
        new Course
        {
            Id = 2,
            Name = "שם קורס 2",
            Description = "טקסט לדוגמה",
            Credits = 4,
            TeacherName = "שם מלא 2"
        },
        new Course
        {
            Id = 3,
            Name = "שם קורס 3",
            Description = "טקסט לדוגמה",
            Credits = 2,
            TeacherName = "שם מלא 3"
        }
    };

    // GET: api/courses
    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAll()
    {
        return Ok(Courses);
    }

    // GET: api/courses/1
    [HttpGet("{id}")]
    public ActionResult<Course> GetById(int id)
    {
        var course = Courses.FirstOrDefault(c => c.Id == id);
        if (course is null)
        {
            return NotFound();
        }
        return Ok(course);
    }
}
