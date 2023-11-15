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
            => _dbContext.Appointments.ToList();

        public async Task CreateAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            await _dbContext.Save();
        }

        public async Task EditAppointment(Appointment appointment, Action<Appointment> onSuccess, Action<string> onFailure)
        {
            if (!_dbContext.Appointments.Any(a => a.AppointmentId == appointment.AppointmentId))
            {
                onFailure("Unable to find a matching appointment with the provided Id");
                return;
            }
            _dbContext.Appointments.Update(appointment);
            await _dbContext.Save();

            onSuccess(appointment);
        }
    }
}
