using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.ValueObjects;

namespace TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Commands;

public record UpdateSubscriptionStatusCommand(int Id, SubscriptionStatus Status);
