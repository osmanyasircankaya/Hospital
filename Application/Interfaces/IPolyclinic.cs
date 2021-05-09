using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Interfaces
{
    public interface IPolyclinic
    {
        IPolyclinicRepository Polyclinics { get; }
    }
}
