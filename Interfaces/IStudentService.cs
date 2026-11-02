using Core.Models;

namespace Core.Interfaces;

public interface IStudentService
{
    List<Student> GetAll();
    List<Student> GetByCourse(int courseId);
    Student? Get(int id);
    void Add(Student student);
    void Update(Student student);
    void Delete(int id);
    int Count { get; }
}
