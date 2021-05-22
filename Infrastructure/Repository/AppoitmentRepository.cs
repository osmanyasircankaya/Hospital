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
    public class AppoitmentRepository : IAppoitmentRepository
    {
        private readonly IConfiguration configuration;
        public AppoitmentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Appoitment entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Appoitment (AppoitmentDate, IsEmpty, DoctorId, PatientId, AddedOn) " +
                        "VALUES (@AppoitmentDate, @IsEmpty, @DoctorId, @PatientId, @AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        //We have a problem 
        public Task<Appoitment> DeleteAppoitmentByPatientIdAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Appoitment WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        //Denemesi yapılacak
        public async Task<IReadOnlyList<Appoitment>> GetAppoitmentsByPatientIdAsync(string patientId)
        {
            var sql = "SELECT * FROM Appoitment WHERE PatientId = @patientId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Appoitment>(sql, new { PatientId = patientId });
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Appoitment>> GetAllAsync()
        {
            var sql = "SELECT * FROM Appoitment";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Appoitment>(sql);
                return result.ToList();
            }
        }

        public async Task<Appoitment> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Appoitment WHERE PatientId = @PatientId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Appoitment>(sql, new { PatientId = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Appoitment entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Appoitment SET AppoitmentDate = @AppoitmentDate, IsEmpty = @IsEmpty, DoctorId = @DoctorId, " +
                         "PatientId = @PatientId, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
