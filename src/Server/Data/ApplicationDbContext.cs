﻿using AppointmentBooking.Server.Models;
using AppointmentBooking.Shared.Interfaces;
using AppointmentBooking.Shared.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AppointmentBooking.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public IQueryable<Appointment> AppointmentsDataSet => Appointments.AsQueryable();
    }
}