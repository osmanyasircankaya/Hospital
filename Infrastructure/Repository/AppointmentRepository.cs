using Dapper;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IConfiguration configuration;
        public AppointmentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> DeleteAppointmentByIdAsync(string patientId)
        {
            var sql = "DELETE FROM Appointment WHERE PatientId = @PatientId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { PatientId = patientId });
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Appointment WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Appointment>> GetAppointmentsByPatientIdAsync(string patientId)
        {
            var sql = "SELECT * FROM Appointment WHERE PatientId = @patientId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Appointment>(sql, new { PatientId = patientId });
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Appointment>> GetAllAsync(string sqlCommand=null)
        {
            var sql = "SELECT * FROM Appointment";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Appointment>(sql);
                return result.ToList();
            }
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Appointment WHERE PatientId = @PatientId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Appointment>(sql, new { PatientId = id });
                return result;
            }
        }

        public async Task<int> UpsertAsync(Appointment entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.ModifiedOn = DateTime.Now;
            var sql = "IF EXISTS " +
                "(SELECT * FROM Appointment WHERE Id = @Id) " +
                "UPDATE Appointment SET AppointmentDate = @AppointmentDate, IsEmpty = @IsEmpty, DoctorId = @DoctorId, " +
                "PatientId = @PatientId, ModifiedOn = @ModifiedOn  WHERE Id = @Id " +
                "ELSE " +
                "INSERT INTO Appointment (AppointmentDate, IsEmpty, DoctorId, PatientId, AddedOn) VALUES (@AppointmentDate, @IsEmpty, @DoctorId, @PatientId, @AddedOn) ";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> GetAppointmentsSizeByDoctorId(int doctorId,string date=null)
        {
            var defaultSql = "Select count(*) from Appointment where doctorId=@doctorId";
            var adding = defaultSql+$"and AppointmentDate Between '{date}' and GETDATE()";
            var sql = date == null ? defaultSql : adding;
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<int>(sql,new {doctorId=doctorId });
                return result.First();
            }
        }

        public async Task<int> GetAppoimentsSizeByPolId(int polId, string date=null)
        {
            var sql= $"Select count(*) from Appointment where polId=@polId and AppointmentDate Between '{date}' and GETDATE()";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<int>(sql, new { polId = polId });
                return result.First();
            }
        }

    }
}
