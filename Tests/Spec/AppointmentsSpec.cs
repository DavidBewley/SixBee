using AppointmentBooking.Shared.Interfaces;
using AppointmentBooking.Shared.Models;
using AppointmentBooking.Shared.Processors;
using AppointmentBooking.Spec.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

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

                _dbContext.Setup<DbSet<Appointment>>(x => x.Appointments)
                    .ReturnsDbSet(new List<Appointment> { (RandomData.Appointment()) }.AsQueryable());

                var processor = new AppointmentProcessor(_dbContext.Object);
                processor.GetAllAppointments();
            }

            [Fact]
            public void AppointmentsAreRetrieved() 
                => _dbContext.Verify(m=>m.Appointments, Times.Once);
        }

        public class WhenNewValidAppointmentIsCreated : IAsyncLifetime
        {
            private readonly Mock<IDbContext> _dbContext;
            private readonly Appointment _appointment;

            public WhenNewValidAppointmentIsCreated()
            {
                _dbContext = new Mock<IDbContext>();

                _dbContext.Setup<DbSet<Appointment>>(x => x.Appointments)
                    .ReturnsDbSet(new List<Appointment> { (RandomData.Appointment()) }.AsQueryable());

                _appointment = RandomData.Appointment();

                var processor = new AppointmentProcessor(_dbContext.Object);
                processor.GetAllAppointments();
            }

            public async Task InitializeAsync()
            {
                var processor = new AppointmentProcessor(_dbContext.Object);
                await processor.CreateAppointment(_appointment);
            }

            public Task DisposeAsync() 
                => Task.CompletedTask;

            [Fact]
            public void AppointmentsIsCreated() 
                => _dbContext.Object.Appointments.Should().ContainEquivalentOf(_appointment);
        }
    }
}
