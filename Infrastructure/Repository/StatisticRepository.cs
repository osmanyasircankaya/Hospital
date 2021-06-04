using Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class StatisticRepository:IStatisticRepository
    {
        private readonly IConfiguration configuration;
        public StatisticRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<dynamic>> GetAppointmentCountByHours(int startHour, int endHour)
        {
            var sql = "FindAppointmentCountWithHour";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                var values = new { StartTime = startHour, EndTime = endHour };
                List<dynamic> result = (await connection.QueryAsync(sql, values, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
        }

        public async Task<int> GetAppointmentsCount()
        {
            var sql = "SELECT COUNT(*) FROM Appointment";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                var result = await connection.QueryAsync<int>(sql);
                return result.First();
            }
        }

        public async Task<List<dynamic>> GetAppointmentsCountOrderByDate()
        {
            var sql = "Select Convert(date, AppointmentDate) AS Date, Count(Convert(date, AppointmentDate)) As AppointmentCount from Appointment Group By Convert(date, AppointmentDate) Order By AppointmentCount Desc";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                List<dynamic> result = (await connection.QueryAsync(sql)).ToList();

                return result;
            }
        }

        public async Task<List<dynamic>> GetAppointmentsDetailByDateRange(string patientId, string startingTime)
        {
            var sql = "GetAppointmentsDetailByDateRange ";

            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                var values = new { PatientId = patientId, StartingTime = startingTime };
                List<dynamic> result = (await connection.QueryAsync(sql, values, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
        }

        public async Task<int> GetAppointmentsSizeByDoctorId(int doctorId, string date)
        {
            var sql = $"Select count(*) from Appointment where doctorId=@doctorId and AppointmentDate Between '{date}' and GETDATE() ";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                var result = await connection.QueryAsync<int>(sql, new { doctorId = doctorId });
                return result.First();
            }
        }

        public async Task<List<dynamic>> GetDoctorsOrderByAppointmentCount()
        {
            var sql = "Select Doctor.FirstName + ' ' + Doctor.LastName as FullName,count(Appointment.DoctorId) as AppointmentCount from Doctor left join Appointment ON Doctor.Id=Appointment.DoctorId Group By Doctor.FirstName + ' ' +Doctor.LastName Order By AppointmentCount DESC";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                var result = await connection.QueryAsync(sql);
                return result.ToList();
            }
        }

        public async Task<dynamic> GetMaximumAppointmentDay()
        {
            var sql = "Select TOP 1 * from  ( Select Convert(date, AppointmentDate) AS Date, Count(Convert(date, AppointmentDate)) As AppointmentCount from Appointment Group By Convert(date, AppointmentDate))As EnYuksekGun Order By AppointmentCount DESC";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                var result = await connection.QueryFirstAsync(sql);

                return result;
            }
        }

        public async Task<dynamic> GetMinimumAppointmentDay()
        {
            var sql = "Select TOP 1 * from  ( Select Convert(date, AppointmentDate) AS Date, Count(Convert(date, AppointmentDate)) As AppointmentCount from Appointment Group By Convert(date, AppointmentDate))As EnDusukGun Order By AppointmentCount ASC";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();
                dynamic result = await connection.QueryFirstAsync(sql);

                return result;
            }
        }

        public async Task<List<dynamic>> GetPolyclinicNameByAppointmentCount()
        {
            var sql = "FindAppointmentCountWithPolyclinicName";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();

                List<dynamic> result = (await connection.QueryAsync(sql, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
        }

        public async Task<List<dynamic>> GetWeekDayByAppointmentCount()
        {
            var sql = "FindWeekDayWithAppointmentCount";
            using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
            {
                connection.Open();

                List<dynamic> result = (await connection.QueryAsync(sql, commandType: CommandType.StoredProcedure)).ToList();

                return result;
            }
        }
    }
}
