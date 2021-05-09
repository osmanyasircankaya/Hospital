using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repository
{
    public class DoctorService : IDoctor
    {
        public DoctorService(IDoctorRepository doctorRepository)
        {
            Doctors = doctorRepository;
        }
        public IDoctorRepository Doctors { get; }
    }
}
