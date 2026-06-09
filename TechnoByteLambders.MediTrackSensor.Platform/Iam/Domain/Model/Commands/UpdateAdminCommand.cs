namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Commands;

public record UpdateAdminCommand(
    int Id,
    string EntityName,
    string EntityCode,
    string Schedule);
