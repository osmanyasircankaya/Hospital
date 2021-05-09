using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repository
{
    public class PolyclinicService : IPolyclinic
    {
        public PolyclinicService(IPolyclinicRepository polyclinicRepository)
        {
            Polyclinics = polyclinicRepository;
        }
        public IPolyclinicRepository Polyclinics { get; }
    }
}
