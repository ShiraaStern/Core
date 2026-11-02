using Core.Models;

namespace Core.Interfaces;

public interface ICourseService
{
    List<Course> GetAll();
    Course? Get(int id);
    void Add(Course course);
    void Update(Course course);
    void Delete(int id);
    int Count { get; }
}
