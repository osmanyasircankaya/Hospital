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
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration configuration;
        public PatientRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Patient entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Patient (Id,AddedOn) VALUES (@Id,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Patient WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Patient>> GetAllAsync()
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

        public async Task<int> UpdateAsync(Patient entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Patient SET Id = @Id, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
