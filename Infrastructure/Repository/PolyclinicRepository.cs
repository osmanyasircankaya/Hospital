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
    public class PolyclinicRepository : IPolyclinicRepository
    {
        private readonly IConfiguration configuration;
        public PolyclinicRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Polyclinic entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Polyclinics (Name, AddedOn) VALUES (@Name,@AddedOn)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Polyclinics WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Polyclinic>> GetAllAsync()
        {
            var sql = "SELECT * FROM Polyclinics";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Polyclinic>(sql);
                return result.ToList();
            }
        }

        public async Task<Polyclinic> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Polyclinics WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Polyclinic>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Polyclinic entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Polyclinics SET Name = @Name, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
