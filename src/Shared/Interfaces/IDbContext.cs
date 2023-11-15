using AppointmentBooking.Shared.Models;

namespace AppointmentBooking.Shared.Interfaces
{
    public interface IDbContext
    {
        IQueryable<Appointment> AppointmentsDataSet { get; }
    }
}
