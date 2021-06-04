using Dapper;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IConfiguration configuration;
        public DoctorRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DeleteDoctor";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id },commandType:System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IReadOnlyList<Doctor>> GetAllAsync(string sqlCommand=null)
        {
            var sql = "SELECT * FROM Doctor";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Doctor>(sql);
                return result.ToList();
            }
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var sql = "GetDoctorById";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Doctor>(sql, new { Id = id },commandType:System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IReadOnlyList<Doctor>> GetDoctorsByPolIdAsync(int polId)
        {
            var sql = "GetDoctorsByPolId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Doctor>(sql, new { PolId = polId },commandType:System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<dynamic>> GetDoctorsOrderByAppointmentCount()
        {
            var sql = "Select Doctor.FirstName + ' ' + Doctor.LastName as FullName,count(Appointment.DoctorId) as AppointmentCount from Doctor left join Appointment ON Doctor.Id=Appointment.DoctorId Group By Doctor.FirstName + ' ' +Doctor.LastName Order By AppointmentCount DESC";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync(sql);
                return result.ToList();
            }
        }

        public async Task<int> UpsertAsync(Doctor entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.ModifiedOn = DateTime.Now;
            var sql = "IF EXISTS " +
                "(SELECT * FROM Doctor WHERE Id = @Id) UPDATE Doctor SET FirstName = @FirstName, LastName = @LastName, Mail = @Mail, PolId = @PolId, ModifiedOn = @ModifiedOn WHERE Id = @Id " +
                "ELSE " +
                "INSERT INTO Doctor (FirstName, LastName, Mail, PolId, AddedOn) VALUES (@FirstName, @LastName, @Mail, @PolId, @AddedOn) ";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
