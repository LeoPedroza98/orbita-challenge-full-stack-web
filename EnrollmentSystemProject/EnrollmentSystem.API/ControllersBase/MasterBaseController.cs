using EnrollmentSystem.Domain.BaseEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EnrollmentSystem.API.ControllersBase;

[Route("api/[controller]")]
[ApiController]
public class MasterBaseController  : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected PageResultDomain GetPageResult<TEntity>(IQueryable query, ODataQueryOptions<TEntity> options) where TEntity : class, IEntity
    {
        var odataSettings = new ODataQuerySettings();

        if (options.Filter != null)
            query = options.Filter.ApplyTo(query, odataSettings);

        var count = (query as IQueryable<TEntity>)?.LongCount();

        if (options.OrderBy != null)
            query = options.OrderBy.ApplyTo(query, odataSettings);

        if (options.Skip != null)
            query = options.Skip.ApplyTo(query, odataSettings);

        if (options.Top != null)
            query = options.Top.ApplyTo(query, odataSettings);

        if (options.SelectExpand != null)
        {
            if (!options.SelectExpand.Context.DefaultQueryConfigurations.EnableExpand)
                throw new System.InvalidOperationException("Informe o include via header!");

            query = options.SelectExpand.ApplyTo(query, odataSettings);
        }

        return new PageResultDomain(query, count ?? 0);
    }
}