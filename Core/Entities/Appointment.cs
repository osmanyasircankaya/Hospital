using System;

namespace Hospital.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsEmpty { get; set; }
        public int DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
