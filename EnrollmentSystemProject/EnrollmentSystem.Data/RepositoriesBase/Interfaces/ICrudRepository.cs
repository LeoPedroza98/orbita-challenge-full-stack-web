using EnrollmentSystem.Domain.BaseEntities;

namespace EnrollmentSystem.Data.RepositoriesBase.Interfaces;

public interface ICrudRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class, IEntity
{
    Task Insert(TEntity entity);
    void Update(TEntity entity);
    Task Remove(long id);
    void Remove(TEntity entity);
    void Detached(TEntity entity);
    Task SaveChangesAsync();
}