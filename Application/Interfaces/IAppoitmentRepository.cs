using Hospital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IAppoitmentRepository : IGenericRepository<Appoitment>
    {
        Task<IReadOnlyList<Appoitment>> GetAppoitmentsByPatientIdAsync(int patientId);
        Task<Appoitment> DeleteAppoitmentByPatientIdAsync(int patientId);
    }
}
