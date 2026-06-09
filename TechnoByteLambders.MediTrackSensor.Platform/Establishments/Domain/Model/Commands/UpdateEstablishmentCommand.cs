using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.ValueObjects;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Commands;

public record UpdateEstablishmentCommand(
    int Id,
    string EstablishmentName,
    EstablishmentType EstablishmentType,
    Address Address,
    Location Location,
    string Phone,
    string Email,
    string Website);
