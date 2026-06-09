using Microsoft.EntityFrameworkCore;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Model.ValueObjects;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<IEnumerable<Subscription>> FindByAdminIdAsync(
        int adminId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.AdminId == new AdminId(adminId))
            .ToListAsync(cancellationToken);
    }
}
