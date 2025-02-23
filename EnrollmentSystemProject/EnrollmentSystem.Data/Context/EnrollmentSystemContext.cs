using EnrollmentSystem.Domain.Entities;
using EnrollmentSystem.Domain.IdentityBase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystem.Data.Context;

public class EnrollmentSystemContext : EnrollmentSystemContextBase<User, Role, string>
{
    private readonly IUserSession _userSession;

    public EnrollmentSystemContext(DbContextOptions options, IUserSession userSession) : base(options, userSession)
    {
        _userSession = userSession;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #region DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    #endregion
}