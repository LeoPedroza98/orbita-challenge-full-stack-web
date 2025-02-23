namespace EnrollmentSystem.Domain.Entities;

public class UsuarioToken
{
    public bool Authenticated { get; set; }
    public string DateCreated { get; set; }
    public string DateExpiration { get; set; }
    public string TokenAcess { get; set; }
    public string Message { get; set; }
    
}