using EnrollmentSystem.Domain.BaseEntities;

namespace EnrollmentSystem.Domain.Entities;

public class Enrollment : IEntity
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public long CourseId  { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    public Student Student { get; set; }
    public Course Course{ get; set; }
}