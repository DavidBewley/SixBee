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
                => _dbContext.Verify(m => m.Appointments, Times.Once);
        }

        public class WhenNewValidAppointmentIsCreated : IAsyncLifetime
        {
            private Mock<IDbContext> _dbContext;
            private Appointment _appointment;

            public async Task InitializeAsync()
            {
                _appointment = RandomData.Appointment();

                _dbContext = new Mock<IDbContext>();

                _dbContext.Setup<DbSet<Appointment>>(x => x.Appointments)
                    .ReturnsDbSet(new List<Appointment> { });

                var processor = new AppointmentProcessor(_dbContext.Object);
                await processor.CreateAppointment(_appointment);
            }

            public Task DisposeAsync()
                => Task.CompletedTask;

            [Fact]
            public void AppointmentsIsCreated()
                => _dbContext.Verify(a => a.Appointments.Add(_appointment), Times.Once);
        }

        public class UpdateAppointmentBase : IAsyncLifetime
        {
            protected Mock<IDbContext> DbContext = new Mock<IDbContext>();
            protected Appointment AppointmentEdit;
            protected Appointment? Response;
            protected string? NotFoundMessage;

            public async Task InitializeAsync()
            {
                var processor = new AppointmentProcessor(DbContext.Object);
                await processor.EditAppointment(
                    appointment: AppointmentEdit,
                    onSuccess: response => Response = response,
                    onFailure: message => NotFoundMessage = message
                    );
            }

            public Task DisposeAsync()
                => Task.CompletedTask;
        }

        public class WhenUpdateCalledWithNoAppointmentToUpdate : UpdateAppointmentBase
        {
            public WhenUpdateCalledWithNoAppointmentToUpdate()
            {
                DbContext.Setup<DbSet<Appointment>>(x => x.Appointments)
                    .ReturnsDbSet(new List<Appointment> { }.AsQueryable());

                AppointmentEdit = RandomData.Appointment();
            }

            [Fact]
            public void AppointmentIsSearchedFor()
                => DbContext.Verify(m => m.Appointments, Times.Once);

            [Fact]
            public void OnFailureMessageIsReturned()
                => NotFoundMessage.Should().Be("Unable to find a matching appointment with the provided Id");
        }

        public class WhenUpdateCalledWithValidAppointmentToUpdate : UpdateAppointmentBase
        {
            public WhenUpdateCalledWithValidAppointmentToUpdate()
            {
                AppointmentEdit = RandomData.Appointment();
                var oldAppointment = RandomData.Appointment();
                oldAppointment.AppointmentId = AppointmentEdit.AppointmentId;

                DbContext.Setup<DbSet<Appointment>>(x => x.Appointments)
                    .ReturnsDbSet(new List<Appointment> { oldAppointment }.AsQueryable());
            }

            [Fact]
            public void AppointmentIsSearchedFor()
                => DbContext.Verify(m => m.Appointments, Times.AtLeastOnce);

            [Fact]
            public void NewAppointmentIsReturned()
                => Response.Should().BeEquivalentTo(AppointmentEdit);
        }
    }
}
