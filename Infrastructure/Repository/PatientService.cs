using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repository
{
    public class PatientService : IPatient
    {
        public PatientService(IPatientRepository patientRepository)
        {
            Patients = patientRepository;
        }
        public IPatientRepository Patients { get; }
    }
}
