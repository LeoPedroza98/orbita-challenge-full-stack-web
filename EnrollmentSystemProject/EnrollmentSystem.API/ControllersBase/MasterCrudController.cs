﻿using EnrollmentSystem.Domain.BaseEntities;
using EnrollmentSystem.Services.ServicesBase.Interfaces;
using EnrollmentSystem.Utils.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystem.API.ControllersBase;

public class MasterCrudController<TEntity> : MasterQueryController<TEntity> where TEntity : class, IEntity
{
    private readonly ICrudService<TEntity> _service;
    protected readonly string _includePatch;
    
    public MasterCrudController(ILogger<MasterCrudController<TEntity>> logger, ICrudService<TEntity> service, string includePatch = "") : base(logger, service)
    {
        _service = service;
        _includePatch = includePatch;
    }
    
    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Post([FromBody] TEntity model)
    {
        try
        {
            await _service.Post(model);

            return Created(string.Empty, model);
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
    
    [HttpPatch("{id}")]
    public virtual async Task<IActionResult> Patch(long id, [FromBody] JsonPatchDocument<TEntity> model)
    {
        try
        {
            model.Operations.RemoveAll(x => x.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Test);

            await _service.Patch(id, model, _includePatch);

            return Ok(MensagemHelper.OperacaoSucesso);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
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
    
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _service.Delete(id);

            return Ok(MensagemHelper.OperacaoSucesso);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
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