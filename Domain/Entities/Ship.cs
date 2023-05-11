using Domain.Common;
using NetTopologySuite.Geometries;

namespace Domain.Entities
{
    public class Ship : BaseEntity
    {
        public string Name { get; set; }
        public Point? Location { get; set; }
        public double? Velocity { get; set; } // unit: km/h

        public Ship(string name)
        {
            Name = name;
        }

        public Ship(string name, Point? location, double? velocity) : this(name)
        {
            Location = location;
            Velocity = velocity;
        }
    }
}
