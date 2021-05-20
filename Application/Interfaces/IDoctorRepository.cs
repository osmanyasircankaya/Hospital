using Hospital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
         Task<IReadOnlyList<Doctor>> GetDoctorsByPolIdAsync(int polId);
    }
}
