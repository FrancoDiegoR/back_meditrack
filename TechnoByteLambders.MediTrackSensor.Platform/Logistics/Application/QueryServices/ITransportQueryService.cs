using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Domain.Model.Queries;

namespace TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.QueryServices;

public interface ITransportQueryService
{
    Task<IEnumerable<Transport>> Handle(GetAllTransportsQuery query, CancellationToken cancellationToken = default);
    Task<Transport?> Handle(GetTransportByIdQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transport>> Handle(GetTransportsByEstablishmentIdQuery query, CancellationToken cancellationToken = default);
}
