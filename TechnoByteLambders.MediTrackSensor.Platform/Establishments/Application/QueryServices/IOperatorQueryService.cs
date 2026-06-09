using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Queries;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.QueryServices;

public interface IOperatorQueryService
{
    Task<IEnumerable<Operator>> Handle(GetAllOperatorsQuery query, CancellationToken cancellationToken = default);
    Task<Operator?> Handle(GetOperatorByIdQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<Operator>> Handle(GetOperatorsByEstablishmentIdQuery query, CancellationToken cancellationToken = default);
}
