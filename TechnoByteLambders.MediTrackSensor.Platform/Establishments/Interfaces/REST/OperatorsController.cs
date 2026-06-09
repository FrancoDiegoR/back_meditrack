using Microsoft.AspNetCore.Mvc;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Commands;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST.Resources;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST.Transform;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class OperatorsController(
    IOperatorCommandService commandService,
    IOperatorQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await queryService.Handle(new GetAllOperatorsQuery(), ct);
        return Ok(items.Select(OperatorResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOperatorResource resource, CancellationToken ct)
    {
        var result = await commandService.Handle(
            new CreateOperatorCommand(resource.Schedule, resource.EstablishmentId, resource.UsersId), ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return Ok(OperatorResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateOperatorResource resource, CancellationToken ct)
    {
        var result = await commandService.Handle(new UpdateOperatorCommand(id, resource.Schedule, resource.EstablishmentId), ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return Ok(OperatorResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpPut("{id:int}/alert-answered")]
    public async Task<IActionResult> IncrementAlert(int id, CancellationToken ct)
    {
        var result = await commandService.IncrementAlertAsync(id, ct);
        if (result.IsFailure) return NotFound(new { error = ((dynamic)result).Error });
        return Ok(OperatorResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await commandService.DeleteAsync(id, ct);
        if (result.IsFailure) return NotFound(new { error = ((dynamic)result).Error });
        return NoContent();
    }
}
