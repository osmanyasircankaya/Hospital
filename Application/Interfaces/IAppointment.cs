namespace Hospital.Application.Interfaces
{
    public interface IAppointment
    {
        IAppointmentRepository Appointments { get; }
    }
}
