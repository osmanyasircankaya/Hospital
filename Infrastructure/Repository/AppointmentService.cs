using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repository
{
    public class AppointmentService : IAppoitment
    {
        public AppointmentService(IAppointmentRepository AppoitmentRepository)
        {
            Appoitments = AppoitmentRepository;
        }
        public IAppointmentRepository Appoitments { get; }
    }
}
