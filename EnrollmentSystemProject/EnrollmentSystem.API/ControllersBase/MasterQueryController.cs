using EnrollmentSystem.Domain.BaseEntities;
using EnrollmentSystem.Services.ServicesBase.Interfaces;
using EnrollmentSystem.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EnrollmentSystem.API.ControllersBase;

public class MasterQueryController<TEntity> : MasterBaseController where TEntity : class, IEntity
{
    protected readonly ILogger<MasterQueryController<TEntity>> _logger;
    private readonly IQueryService<TEntity> _service;

    public MasterQueryController(ILogger<MasterQueryController<TEntity>> logger, IQueryService<TEntity> service) :
        base()
    {
        _logger = logger;
        _service = service;
    }

    protected IQueryable<TEntity> GetFiltered(IQueryable query, ODataQueryOptions<TEntity> options)
    {
        var odataSettings = new ODataQuerySettings();

        if (options.Filter != null)
            query = options.Filter.ApplyTo(query, odataSettings);

        if (options.OrderBy != null)
            query = options.OrderBy.ApplyTo(query, odataSettings);

        if (options.Skip != null)
            query = options.Skip.ApplyTo(query, odataSettings);

        if (options.Top != null)
            query = options.Top.ApplyTo(query, odataSettings);

        if (options.SelectExpand != null)
            query = options.SelectExpand.ApplyTo(query, odataSettings);

        return query.Cast<TEntity>();
    }

    [HttpGet]
    public virtual ActionResult<List<TEntity>> Get(ODataQueryOptions<TEntity> options,
        [FromHeader] string include = null)
    {
        try
        {
            var pageResult = GetPageResult(_service.Get(include), options);

            return Ok(pageResult);
        }
        catch (Exception e)
        {
            _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

            return StatusCode(StatusCodes.Status500InternalServerError,
                $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
        }
    }

    [HttpGet("listar")]
    public virtual ActionResult<List<TEntity>> GetNoInclude(ODataQueryOptions<TEntity> options)
    {
        try
        {
            var pageResult = GetPageResult(_service.GetNoInclude(), options);

            return Ok(pageResult);
        }
        catch (Exception e)
        {
            _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

            return StatusCode(StatusCodes.Status500InternalServerError,
                $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
        }
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> GetById(long id)
    {
        try
        {
            var domain = await _service.Get(id);

            if (domain == null)
                return NotFound(MensagemHelper.RegistroNaoEncontrato);

            return Ok(domain);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("{0} - {1}", e.Message, e.InnerException?.Message);

            return StatusCode(StatusCodes.Status500InternalServerError,
                $"{MensagemHelper.AlgumErroOcorreu} {e.Message} - {e.InnerException?.Message}");
        }
    }
}