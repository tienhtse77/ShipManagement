using Application.Common.Models.Mapping;
using Domain.Entities;

namespace Application.Ports.Queries.GetClosestPort
{
    public class ClosestPortDto : IMapFrom<Port>
    {
        public PortDto Port { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Port, ClosestPortDto>()
                .ForMember(c => c.Port, m => m.MapFrom(v => v));
        }
    }

    public class PortDto : IMapFrom<Port>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LocationVm Location { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Port, PortDto>()
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
