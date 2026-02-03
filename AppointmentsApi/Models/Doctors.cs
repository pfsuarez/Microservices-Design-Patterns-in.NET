namespace AppointmentsApi.Models;

public record class Doctors(Guid DoctorId, string FirstName, string LastName, string Specialty);