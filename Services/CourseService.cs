using System.Text.Json;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Hosting;

namespace Core.Services;

public class CourseService : ICourseService
{
    private readonly List<Course> courses;
    private readonly string filePath;

    public CourseService(IWebHostEnvironment webHost)
    {
        filePath = Path.Combine(webHost.ContentRootPath, "Data", "courses.json");

        using var jsonFile = File.OpenText(filePath);
        courses = JsonSerializer.Deserialize<List<Course>>(
                      jsonFile.ReadToEnd(),
                      new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                  ?? new List<Course>();
    }

    private void SaveToFile()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        File.WriteAllText(filePath, JsonSerializer.Serialize(courses, options));
    }

    public List<Course> GetAll() => courses;

    public Course? Get(int id) => courses.FirstOrDefault(c => c.Id == id);

    public void Add(Course course)
    {
        course.Id = courses.Count == 0 ? 1 : courses.Max(c => c.Id) + 1;
        courses.Add(course);
        SaveToFile();
    }

    public void Update(Course course)
    {
        var index = courses.FindIndex(c => c.Id == course.Id);
        if (index == -1)
            return;

        courses[index] = course;
        SaveToFile();
    }

    public void Delete(int id)
    {
        var course = Get(id);
        if (course is null)
            return;

        courses.Remove(course);
        SaveToFile();
    }

    public int Count => courses.Count;
}
