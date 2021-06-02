using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStatisticRepository
    {
        Task<int> GetAppointmentsCount();
        Task<int> GetAppointmentsSizeByDoctorId(int doctorId, string date);
        Task<List<dynamic>> GetAppointmentsCountOrderByDate();
        Task<dynamic> GetMinimumAppointmentDay();
        Task<dynamic> GetMaximumAppointmentDay();
        Task<List<dynamic>> GetAppointmentsDetailByDateRange(string patientId, string startingTime);
        Task<List<dynamic>> GetAppointmentCountByHours(int startHour, int endHour);
        Task<List<dynamic>> GetWeekDayByAppointmentCount();
        Task<List<dynamic>> GetDoctorsOrderByAppointmentCount();
        Task<List<dynamic>> GetPolyclinicNameByAppointmentCount();

    }
}
