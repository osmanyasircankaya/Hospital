using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IReadOnlyList<Appointment>> GetAppointmentsByPatientIdAsync(string patientId);
        Task<int> DeleteAppointmentByIdAsync(string patientId);
        Task<int> GetAppointmentsSizeByDoctorId(int doctorId,string date=null);
        Task<int> GetAppoimentsSizeByPolId(int polId, string date=null);
    }
}
