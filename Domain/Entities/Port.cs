using Domain.Common;
using NetTopologySuite.Geometries;

namespace Domain.Entities
{
    public class Port : BaseEntity
    {
        public string Name { get; set; }
        public Point Location { get; set; }

        public Port(string name, Point location)
        {
            Name = name;
            Location = location;
        }
    }
}
