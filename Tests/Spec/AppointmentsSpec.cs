using AppointmentBooking.Shared.Interfaces;
using AppointmentBooking.Shared.Models;
using AppointmentBooking.Shared.Processors;
using AppointmentBooking.Spec.Helpers;
using Moq;

namespace AppointmentBooking.Spec
{
    public class AppointmentsSpec
    {
        public class WhenAllAppointmentsAreRequested
        {
            private readonly Mock<IDbContext> _dbContext;

            public WhenAllAppointmentsAreRequested()
            {
                _dbContext = new Mock<IDbContext>();
                _dbContext
                    .Setup(db => db.AppointmentsDataSet)
                    .Returns(new List<Appointment> { (RandomData.Appointment()) }.AsQueryable());

                var processor = new AppointmentProcessor(_dbContext.Object);
                processor.GetAllAppointments();
            }

            [Fact]
            public void AppointmentsAreRetrieved() 
                => _dbContext.Verify(m=>m.AppointmentsDataSet, Times.Once);
        }
    }
}
