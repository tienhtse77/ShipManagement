using Application.Common.Models.Mapping;
using Domain.Entities;
using NetTopologySuite.Geometries;

namespace Application.Ships.Queries.GetAllShips
{
    public class ShipDto : IMapFrom<Ship>
    {
        public string Name { get; set; }
        public LocationVm? Location { get; set; }
        public double? Velocity { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Ship, ShipDto>()
                .ForMember(c => c.Location, m => m.MapFrom(v => v.Location != null
                    ? new LocationVm() { Latitude = v.Location.X, Longitude = v.Location.Y }
                    : null));
        }
    }

    public class LocationVm
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}
