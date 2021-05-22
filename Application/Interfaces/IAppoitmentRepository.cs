using Hospital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IAppoitmentRepository : IGenericRepository<Appoitment>
    {
        Task<IReadOnlyList<Appoitment>> GetAppoitmentsByPatientIdAsync(string patientId);
        Task<Appoitment> DeleteAppoitmentByPatientIdAsync(int patientId);
    }
}
