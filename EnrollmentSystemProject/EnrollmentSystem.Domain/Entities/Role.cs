using Microsoft.AspNetCore.Identity;

namespace EnrollmentSystem.Domain.Entities;

public class Role : IdentityRole
{
    public Role() { }
    public Role(string name):base(name) { }
}
