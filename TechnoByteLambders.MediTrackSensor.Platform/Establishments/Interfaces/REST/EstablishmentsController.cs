using Microsoft.AspNetCore.Mvc;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Commands;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.ValueObjects;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST.Resources;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST.Transform;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class EstablishmentsController(
    IEstablishmentCommandService commandService,
    IEstablishmentQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await queryService.Handle(new GetAllEstablishmentsQuery(), ct);
        return Ok(items.Select(EstablishmentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEstablishmentResource resource, CancellationToken ct)
    {
        var command = new CreateEstablishmentCommand(
            resource.EstablishmentName, resource.EstablishmentType,
            new Address(resource.Address, resource.District, resource.CityRegion, resource.Country),
            new Location(resource.Latitude, resource.Longitude),
            resource.Phone, resource.Email, resource.Website, resource.AdminId);

        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return Ok(EstablishmentResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await commandService.DeleteAsync(id, ct);
        if (result.IsFailure) return NotFound(new { error = ((dynamic)result).Error });
        return NoContent();
    }
}
