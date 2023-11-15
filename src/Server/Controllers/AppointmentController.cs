using AppointmentBooking.Shared.Processors;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.Server.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentProcessor _appointmentProcessor;

        public AppointmentController(AppointmentProcessor appointmentProcessor)
        {
            _appointmentProcessor = appointmentProcessor;
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
            => Ok(_appointmentProcessor.GetAllAppointments());
    }
}
