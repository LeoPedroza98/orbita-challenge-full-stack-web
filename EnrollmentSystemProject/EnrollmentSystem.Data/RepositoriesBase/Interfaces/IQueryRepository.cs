using EnrollmentSystem.Domain.BaseEntities;

namespace EnrollmentSystem.Data.RepositoriesBase.Interfaces;

public interface IQueryRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> GetByAsync(long id);
    Task<TEntity> GetByIdAsync(long id, string include);
    Task<TEntity> GetByIdTrackingAsync(long id);
    Task<TEntity> GetByIdTrackingAsync(long id, string include);
    Task<TEntity> GetByIdNoIncludeAsync(long id);
    Task<TEntity> GetByIdTrackingNoFilterAsync(long id);
    IQueryable<TEntity> GetAll(string include = "");
    IQueryable<TEntity> GetAllNoInclude();
    Task<TEntity> GetByIdAsync(long id);
}