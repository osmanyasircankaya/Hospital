﻿using Dapper;
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

        public async Task<IReadOnlyList<Doctor>> GetDoctorsByPolIdAsync(int polId)
        {
            var sql = "SELECT * FROM Doctor Where PolId=@PolId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Doctor>(sql, new { PolId = polId });
                return result.ToList();
            }
        }

        public async Task<int> UpsertAsync(Doctor entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.ModifiedOn = DateTime.Now;
            var sql = "IF EXISTS " +
                "(SELECT * FROM Doctor WHERE Id = @Id) UPDATE Doctor SET FirstName = @FirstName, LastName = @LastName, PolId = @PolId, ModifiedOn = @ModifiedOn WHERE Id = @Id " +
                "ELSE " +
                "INSERT INTO Doctor (FirstName, LastName, PolId, AddedOn) VALUES (@FirstName, @LastName, @PolId, @AddedOn) ";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
