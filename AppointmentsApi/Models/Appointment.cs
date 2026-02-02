using Microsoft.EntityFrameworkCore;

namespace AppointmentsApi.Models;

[Owned]
public record TimeSlot(DateTime Start, DateTime End);

[Owned]
public record Location(string RoomNumber, string Building);

public class Appointment
{
    public Guid AppointmentId { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public TimeSlot Slot { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public string Purpose { get; set; } = null!;

    public void Reschedule(TimeSlot newSlot)
    {
        Slot = newSlot;
    }

    public void ChangePurpose(string newPurpose)
    {
        Purpose = newPurpose;
    }
}
