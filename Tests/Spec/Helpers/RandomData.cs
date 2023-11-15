using AppointmentBooking.Shared.Models;
using Bogus;

namespace AppointmentBooking.Spec.Helpers
{
    public class RandomData
    {
        public static Appointment Appointment() => new Faker<Appointment>()
            .RuleFor(a => a.AppointmentId, Guid.NewGuid)
            .RuleFor(a => a.AppointmentDateTime, r => r.Date.Future())
            .RuleFor(a => a.Issue, r => r.Random.Words(10))
            .RuleFor(a => a.ContactNumber, r => r.Phone.PhoneNumber())
            .RuleFor(a => a.EmailAddress, r => r.Internet.Email())
            .RuleFor(a => a.IsApproved, r => r.Random.Bool());
    }
}
