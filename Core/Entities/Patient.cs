using System;

namespace Hospital.Core.Entities
{
    public class Patient
    {
        public string Id { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
