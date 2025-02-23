using EnrollmentSystem.Data.RepositoriesBase.Interfaces;
using EnrollmentSystem.Domain.BaseEntities;
using EnrollmentSystem.Services.ServicesBase.Interfaces;
using EnrollmentSystem.Utils.Exceptions;
using Microsoft.AspNetCore.JsonPatch;

namespace EnrollmentSystem.Services.ServicesBase.Entities;

public class CrudService<TEntity, TCrudRepository> : QueryService<TEntity, TCrudRepository>, ICrudService<TEntity> where TEntity : class, IEntity where TCrudRepository : ICrudRepository<TEntity>
{
    public CrudService(TCrudRepository repository) : base(repository)
    {

    }

    public async virtual Task Delete(long id)
    {
        await _repository.Remove(id);
        await SaveChangesAsync();
    }

    public async virtual Task Delete(TEntity entity)
    {
        _repository.Remove(entity);
        await SaveChangesAsync();
    }
    public async virtual Task Post(TEntity entity, bool saveChanges = true)
    {
        await _repository.Insert(entity);

        if (saveChanges)
            await _repository.SaveChangesAsync();
    }

    public async virtual Task Post(TEntity entity)
    {
        await Post(entity, true);
    }
    public async virtual Task Put(TEntity entity)
    {
        _repository.Update(entity);
        await SaveChangesAsync();
    }

    public async virtual Task Patch(TEntity entity)
    {
        await SaveChangesAsync();
    }

    public async virtual Task Patch(long id, JsonPatchDocument<TEntity> model, string include)
    {
        var domain = string.IsNullOrEmpty(include) ? await GetTracking(id) : await GetTracking(id, include);
            
        if (model.Operations.Any(op => op.path == null))
        {
            throw new Exception("Um caminho no documento de patch é nulo.");
        }
            
        if (domain == null)
            throw new NotFoundException(MensagemHelper.RegistroNaoEncontrato);

        model.ApplyTo(domain);

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _repository.SaveChangesAsync();
    }
}