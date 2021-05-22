using System;

namespace Hospital.Core.Entities
{
    public class Appoitment
    {
        public DateTime AppoitmentDate { get; set; }
        public bool IsEmpty { get; set; }
        public int DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
