using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.Internal.QueryServices;

public class EstablishmentQueryService(IEstablishmentRepository establishmentRepository) : IEstablishmentQueryService
{
    public async Task<IEnumerable<Establishment>> Handle(GetAllEstablishmentsQuery query, CancellationToken cancellationToken = default)
        => await establishmentRepository.ListAsync(cancellationToken);

    public async Task<Establishment?> Handle(GetEstablishmentByIdQuery query, CancellationToken cancellationToken = default)
        => await establishmentRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Establishment>> Handle(GetEstablishmentsByAdminIdQuery query, CancellationToken cancellationToken = default)
        => await establishmentRepository.FindByAdminIdAsync(query.AdminId, cancellationToken);
}
