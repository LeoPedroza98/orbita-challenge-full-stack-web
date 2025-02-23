namespace EnrollmentSystem.Domain.IdentityBase.Interfaces;

public class IUserSession
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
    public string UserName { get; set; } 
}