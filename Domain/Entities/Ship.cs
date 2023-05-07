using System.Drawing;

namespace Domain.Entities
{
    public class Ship : BaseEntity
    {
        public string Name { get; set; }
        public Point? Location { get; set; }
        public double? Velocity { get; set; }

        public Ship(string name)
        {
            Name = name;
        }
    }
}
