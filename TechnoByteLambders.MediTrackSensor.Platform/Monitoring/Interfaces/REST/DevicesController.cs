using Microsoft.AspNetCore.Mvc;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Domain.Model.Commands;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Domain.Model.ValueObjects;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Interfaces.REST.Resources;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Interfaces.REST.Transform;

namespace TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class DevicesController(
    IDeviceCommandService commandService,
    IDeviceQueryService queryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await queryService.Handle(new GetAllDevicesQuery(), ct);
        return Ok(items.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceResource resource, CancellationToken ct)
    {
        if (!Enum.TryParse<TypeOfMedication>(resource.TypeOfMedication, true, out var medication))
            return BadRequest(new { error = "Invalid TypeOfMedication value." });

        var result = await commandService.Handle(
            new CreateDeviceCommand(resource.ExactLocation, medication, resource.EstablishmentId, resource.EnabledSensors ?? ""), ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpPut("{id:int}/sensor-data")]
    public async Task<IActionResult> UpdateSensorData(int id, [FromBody] UpdateDeviceSensorDataResource resource, CancellationToken ct)
    {
        if (!Enum.TryParse<DoorStatus>(resource.DoorStatus, true, out var doorStatus))
            return BadRequest(new { error = "Invalid DoorStatus value." });

        var reading = new SensorReading(resource.Temperature, resource.Humidity, resource.LightIntensity,
            resource.AirQuality, resource.Vibration, resource.AtmosphericPressure, resource.SuspendedParticles);

        var result = await commandService.Handle(new UpdateDeviceSensorDataCommand(id, reading, doorStatus), ct);
        if (result.IsFailure) return BadRequest(new { error = ((dynamic)result).Error });
        return Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(((dynamic)result).Value));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await commandService.DeleteAsync(id, ct);
        if (result.IsFailure) return NotFound(new { error = ((dynamic)result).Error });
        return NoContent();
    }
}
