using EnrollmentSystem.Domain.BaseEntities;
using Microsoft.AspNetCore.JsonPatch;

namespace EnrollmentSystem.Services.ServicesBase.Interfaces;

public interface ICrudService<TEntity> : IQueryService<TEntity> where TEntity : class, IEntity
{
    Task Post(TEntity entity, bool saveChanges = true);
    Task Post(TEntity entity);
    Task Put(TEntity entity);
    Task Patch(TEntity entity);
    Task Patch(long id, JsonPatchDocument<TEntity> model, string include);
    Task Delete(long id);
    Task Delete(TEntity entity);
    Task SaveChangesAsync();
}