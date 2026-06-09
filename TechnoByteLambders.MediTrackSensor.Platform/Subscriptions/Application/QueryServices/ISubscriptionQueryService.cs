using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Queries;

namespace TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.QueryServices;

public interface ISubscriptionQueryService
{
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken = default);
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<Subscription>> Handle(GetSubscriptionsByAdminIdQuery query, CancellationToken cancellationToken = default);
}
