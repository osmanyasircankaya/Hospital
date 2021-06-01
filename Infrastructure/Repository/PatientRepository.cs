using Dapper;
using Hospital.Application.Interfaces;
using Hospital.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration configuration;
        public PatientRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DeletePatient";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IReadOnlyList<Patient>> GetAllAsync(string sqlCommand=null)
        {
            var sql = "SELECT * FROM Patient";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Patient>(sql);
                return result.ToList();
            }
        }
        public async Task<Patient> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Patient WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Patient>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(string id)
        {
            var sql = "GetPatientById";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Patient>(sql, new { Id = id },commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpsertAsync(Patient entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.ModifiedOn = DateTime.Now;
            var sql = "IF EXISTS " +
                "(SELECT * FROM Patient WHERE Id = @Id) UPDATE Patient SET Id = @Id, Mail = @Mail, ModifiedOn = @ModifiedOn WHERE Id = @Id " +
                "ELSE " +
                "INSERT INTO Patient (Id, Mail, AddedOn) VALUES (@Id, @Mail, @AddedOn) ";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
