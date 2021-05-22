using Hospital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient> GetPatientByIdAsync(string id);
    }
}
