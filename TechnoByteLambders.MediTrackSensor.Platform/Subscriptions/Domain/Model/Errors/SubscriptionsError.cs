namespace TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Errors;

public enum SubscriptionsError
{
    None,
    SubscriptionNotFound,
    SubscriptionAlreadyExists,
    SubscriptionCreationFailed,
    SubscriptionUpdateFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
