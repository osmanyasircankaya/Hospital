using System;

namespace Hospital.Core.Entities
{
    public class Polyclinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
