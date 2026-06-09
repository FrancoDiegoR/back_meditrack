using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken = default)
        => await subscriptionRepository.ListAsync(cancellationToken);

    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken = default)
        => await subscriptionRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Subscription>> Handle(GetSubscriptionsByAdminIdQuery query, CancellationToken cancellationToken = default)
        => await subscriptionRepository.FindByAdminIdAsync(query.AdminId, cancellationToken);
}
