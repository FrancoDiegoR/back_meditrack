namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Errors;

public enum IamError
{
    None,
    UserNotFound,
    AdminNotFound,
    EmailAlreadyTaken,
    InvalidCredentials,
    UserCreationFailed,
    AdminCreationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
