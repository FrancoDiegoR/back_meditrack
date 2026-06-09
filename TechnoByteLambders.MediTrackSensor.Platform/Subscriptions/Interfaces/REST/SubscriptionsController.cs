using Microsoft.AspNetCore.Mvc;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Commands;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.ValueObjects;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Interfaces.REST.Resources;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Interfaces.REST.Transform;

namespace TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class SubscriptionsController(
    ISubscriptionCommandService commandService,
    ISubscriptionQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await queryService.Handle(new GetAllSubscriptionsQuery(), ct);
        return Ok(items.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetSubscriptionByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpGet("by-admin/{adminId:int}")]
    public async Task<IActionResult> GetByAdmin(int adminId, CancellationToken ct)
    {
        var items = await queryService.Handle(new GetSubscriptionsByAdminIdQuery(adminId), ct);
        return Ok(items.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionResource resource, CancellationToken ct)
    {
        if (!Enum.TryParse<SubscriptionPlan>(resource.Plan, true, out var plan))
            return BadRequest(new { error = "Invalid Plan value." });

        var result = await commandService.Handle(
            new CreateSubscriptionCommand(plan, resource.StartDate, resource.EndDate, resource.AdminId), ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return CreatedAtAction(nameof(GetById), new { id = ((dynamic)result).Value.Id },
            SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateSubscriptionStatusResource resource, CancellationToken ct)
    {
        if (!Enum.TryParse<SubscriptionStatus>(resource.Status, true, out var status))
            return BadRequest(new { error = "Invalid Status value." });

        var result = await commandService.Handle(new UpdateSubscriptionStatusCommand(id, status), ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await commandService.DeleteAsync(id, ct);
        if (result.IsFailure) return NotFound(new { error = ((dynamic)result).Error });
        return NoContent();
    }
}
