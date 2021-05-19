using System;

namespace Hospital.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PolId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
