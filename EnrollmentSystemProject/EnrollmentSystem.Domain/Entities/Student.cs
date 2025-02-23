using EnrollmentSystem.Domain.BaseEntities;

namespace EnrollmentSystem.Domain.Entities;

public class Student : IEntity
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string RA { get; set; }
    public string CPF { get; set; } 
}