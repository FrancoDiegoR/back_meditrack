namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Errors;

public enum EstablishmentsError
{
    None,
    EstablishmentNotFound,
    OperatorNotFound,
    EstablishmentCreationFailed,
    OperatorCreationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
