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

        public async Task<int> AddAsync(Doctor entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Doctor (FirstName, LastName, PolId, AddedOn) VALUES (@FirstName, @LastName, @PolId, @AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Doctor WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Doctor>> GetAllAsync()
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
            var sql = "SELECT * FROM Doctor WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Doctor>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Doctor entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Doctors SET FirstName = @FirstName, LastName = @LastName, PolId = @PolId, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
