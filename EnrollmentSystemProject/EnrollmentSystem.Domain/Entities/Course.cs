using EnrollmentSystem.Domain.BaseEntities;

namespace EnrollmentSystem.Domain.Entities;

public class Course : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int? MaxStudents { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}