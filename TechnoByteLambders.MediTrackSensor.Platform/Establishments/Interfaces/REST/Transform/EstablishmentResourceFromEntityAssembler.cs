using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST.Resources;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Interfaces.REST.Transform;

public static class EstablishmentResourceFromEntityAssembler
{
    public static EstablishmentResource ToResourceFromEntity(Establishment e) =>
        new(e.Id, e.EstablishmentName, e.EstablishmentType.ToString(),
            e.Address.Street, e.Address.District, e.Address.CityRegion, e.Address.Country,
            e.Phone, e.Email, e.Website,
            e.Location.Latitude, e.Location.Longitude,
            e.AdminId.Value, e.CreatedAt);
}
