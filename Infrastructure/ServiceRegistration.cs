using Application.Interfaces;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Repository;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPolyclinicRepository, PolyclinicRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IDoctor, DoctorService>();
            services.AddTransient<IPatient, PatientService>();
            services.AddTransient<IPolyclinic, PolyclinicService>();
            services.AddTransient<IAppointment, AppointmentService>();
            services.AddTransient<IStatistic, StatisticService>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();
        }
    }
}
