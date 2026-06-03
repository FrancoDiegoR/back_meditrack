using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Domain.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Repositories;

public interface IOperatorRepository : IBaseRepository<Operator>
{
    Task<IEnumerable<Operator>> FindByEstablishmentIdAsync(int establishmentId, CancellationToken cancellationToken = default);
}
