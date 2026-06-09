using Microsoft.EntityFrameworkCore;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.ValueObjects;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Infrastructure.Persistence.EFC.Repositories;

public class EstablishmentRepository(AppDbContext context)
    : BaseRepository<Establishment>(context), IEstablishmentRepository
{
    public async Task<IEnumerable<Establishment>> FindByAdminIdAsync(
        int adminId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Establishment>()
            .Where(e => e.AdminId == new AdminId(adminId))
            .ToListAsync(cancellationToken);
    }
}
