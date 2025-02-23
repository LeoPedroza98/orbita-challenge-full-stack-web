using EnrollmentSystem.Domain.IdentityBase.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystem.Data.Context;

public class EnrollmentSystemContextBase<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly IUserSession _userSession;

    public EnrollmentSystemContextBase(DbContextOptions options, IUserSession userSession) : base(options)
    {
        _userSession = userSession;
    }
}