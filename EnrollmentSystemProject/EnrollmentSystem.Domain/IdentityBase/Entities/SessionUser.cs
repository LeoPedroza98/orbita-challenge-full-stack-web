namespace EnrollmentSystem.Domain.IdentityBase.Entities;

public class SessionUser
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
    public string UserName { get; set; }
}