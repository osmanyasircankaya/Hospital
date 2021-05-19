using Hospital.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Repository
{
    public class AppoitmentService : IAppoitment
    {
        public AppoitmentService(IAppoitmentRepository AppoitmentRepository)
        {
            Appoitments = AppoitmentRepository;
        }
        public IAppoitmentRepository Appoitments { get; }
    }
}
