using EnrollmentSystem.Domain.IdentityBase.Services;

namespace EnrollmentSystem.Domain.Entities;

public class User : UserBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool AcessEnabled { get; set; }
    public DateTime DateCreate { get; set; } = DateTime.Now;
}