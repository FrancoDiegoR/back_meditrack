namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Commands;

public record UpdateUserCommand(
    int Id,
    string Name,
    string Phone,
    string JobTitle,
    string Photo);
