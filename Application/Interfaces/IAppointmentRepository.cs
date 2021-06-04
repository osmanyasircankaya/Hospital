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
        Task<int> GetAppointmentsSizeByDoctorId(int doctorId,string date);
        Task<int> GetAppoimentsSizeByPolId(int polId, string date);
        Task<List<dynamic>> GetAppointmentsCountOrderByDate();
        Task<dynamic> GetMinimumAppointmentDay();
        Task<dynamic> GetMaximumAppointmentDay();
        Task<List<dynamic>> GetAppointmentsDetailByDateRange(string patientId, string startingTime);
        Task<List<dynamic>> GetAppointmentCountByHours(int startHour,int endHour);
        Task<List<dynamic>> GetWeekDayByAppointmentCount();
    }
}
