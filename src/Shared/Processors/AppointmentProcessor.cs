using AppointmentBooking.Shared.Interfaces;
using AppointmentBooking.Shared.Models;

namespace AppointmentBooking.Shared.Processors
{
    public class AppointmentProcessor
    {
        private readonly IDbContext _dbContext;
        public AppointmentProcessor(IDbContext context)
        {
            _dbContext = context;
        }

        public List<Appointment> GetAllAppointments() 
            => _dbContext.AppointmentsDataSet.ToList();
    }
}
