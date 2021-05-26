using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repository
{
    public class AppointmentService : IAppointment
    {
        public AppointmentService(IAppointmentRepository AppointmentRepository)
        {
            Appointments = AppointmentRepository;
        }
        public IAppointmentRepository Appointments { get; }
    }
}
