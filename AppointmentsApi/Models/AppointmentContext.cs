using Microsoft.EntityFrameworkCore;

namespace AppointmentsApi.Models;

public class AppointmentContext : DbContext
{
    public AppointmentContext(DbContextOptions<AppointmentContext> options)
        : base(options) { }

    public DbSet<Appointment> Appointments => Set<Appointment>();
}
