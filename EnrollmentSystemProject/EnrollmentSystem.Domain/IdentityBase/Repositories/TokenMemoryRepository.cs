using System.Collections.Concurrent;

namespace EnrollmentSystem.Domain.IdentityBase.Repositories;

public class TokenMemoryRepository
{
    private ConcurrentDictionary<string, string> Dicionario { get; set; }
    
    public TokenMemoryRepository()
    {
        Dicionario = new ConcurrentDictionary<string, string>();
    }
    
    public void Add(string userId, string iat)
    {
        Dicionario.AddOrUpdate(userId, iat, (key, val) => iat);
    }

    public bool TryGet(string userId, out string iat)
    {
        return Dicionario.TryGetValue(userId, out iat);
    }
}