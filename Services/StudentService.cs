using System.Text.Json;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Hosting;

namespace Core.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> students;
    private readonly string filePath;

    public StudentService(IWebHostEnvironment webHost)
    {
        filePath = Path.Combine(webHost.ContentRootPath, "Data", "students.json");

        using var jsonFile = File.OpenText(filePath);
        students = JsonSerializer.Deserialize<List<Student>>(
                       jsonFile.ReadToEnd(),
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? new List<Student>();
    }

    private void SaveToFile()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        File.WriteAllText(filePath, JsonSerializer.Serialize(students, options));
    }

    public List<Student> GetAll() => students;

    public List<Student> GetByCourse(int courseId)
        => students.Where(s => s.CourseId == courseId).ToList();

    public Student? Get(int id) => students.FirstOrDefault(s => s.Id == id);

    public void Add(Student student)
    {
        student.Id = students.Count == 0 ? 1 : students.Max(s => s.Id) + 1;
        students.Add(student);
        SaveToFile();
    }

    public void Update(Student student)
    {
        var index = students.FindIndex(s => s.Id == student.Id);
        if (index == -1)
            return;
        students[index] = student;
        SaveToFile();
    }

    public void Delete(int id)
    {
        var student = Get(id);
        if (student is null)
            return;
        students.Remove(student);
        SaveToFile();
    }

    public int Count => students.Count;
}
