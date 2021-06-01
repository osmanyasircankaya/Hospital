using Hospital.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IPolyclinicRepository : IGenericRepository<Polyclinic>
    {
        Task<List<dynamic>> GetPolyclinicByAppointmentCount();
    }
}
