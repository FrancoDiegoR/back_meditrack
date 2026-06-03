using TechnoByteLambders.MediTrackSensor.Platform.Shared.Domain.Model;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Errors;

public static class EstablishmentsErrors
{
    public static readonly Error EstablishmentNotFound = new("Establishments.EstablishmentNotFound", "Establishment not found.");
    public static readonly Error OperatorNotFound = new("Establishments.OperatorNotFound", "Operator not found.");
    public static readonly Error EstablishmentCreationFailed = new("Establishments.EstablishmentCreationFailed", "An error occurred while creating the establishment.");
    public static readonly Error OperatorCreationFailed = new("Establishments.OperatorCreationFailed", "An error occurred while creating the operator.");
}
