using AppointmentBooking.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Shared.Interfaces
{
    public interface IDbContext
    {
        DbSet<Appointment> Appointments { get; set; }
        Task Save();
    }
}
