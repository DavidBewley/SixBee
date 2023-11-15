using AppointmentBooking.Shared.Models;
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

        [HttpGet]
        [Route("{appointmentId}")]
        public async Task<ActionResult> GetAppointment([FromRoute] Guid appointmentId)
        {
            ActionResult response = NotFound();

            await _appointmentProcessor.GetAppointment(
                appointmentId: appointmentId,
                onSuccess: appointment => response = Ok(appointment),
                onFailure: message => NotFound(message)
            );

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointments([FromBody] Appointment appointment)
        {
            await _appointmentProcessor.CreateAppointment(appointment);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditAppointment([FromBody] Appointment appointment)
        {
            ActionResult response = NotFound();

            await _appointmentProcessor.EditAppointment(
                appointment: appointment,
                onSuccess: appointment => response = Ok(appointment),
                onFailure: message => NotFound(message)
            );

            return response;
        }
    }
}
