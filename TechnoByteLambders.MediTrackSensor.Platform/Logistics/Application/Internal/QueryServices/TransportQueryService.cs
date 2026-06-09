using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Domain.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.Internal.QueryServices;

public class TransportQueryService(ITransportRepository transportRepository) : ITransportQueryService
{
    public async Task<IEnumerable<Transport>> Handle(GetAllTransportsQuery query, CancellationToken cancellationToken = default)
        => await transportRepository.ListAsync(cancellationToken);

    public async Task<Transport?> Handle(GetTransportByIdQuery query, CancellationToken cancellationToken = default)
        => await transportRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Transport>> Handle(GetTransportsByEstablishmentIdQuery query, CancellationToken cancellationToken = default)
        => await transportRepository.FindByEstablishmentIdAsync(query.EstablishmentId, cancellationToken);
}
