using AppointmentsApi.Models;
using AppointmentsApi.Services;
using MassTransit;

namespace AppointmentsApi.Consumer;

public class AppointmentCreatedConsumer(
    PatientsApiClient patientsApiClient,
    DoctorsApiClient doctorsApiClient,
    IEmailService emailService
) : IConsumer<AppointmentCreated>
{
    public async Task Consume(ConsumeContext<AppointmentCreated> context)
    {
        var message = context.Message;

        Console.WriteLine("Retrieve doctors details");
        Console.WriteLine("Send email to doctor");

        // Not implementd yet
        //var doctor = await doctorsApiClient.GetDoctorAsync(message.DoctorId);
        Console.WriteLine($"Doctor Id: {message.DoctorId}");


        Console.WriteLine("Retrieve patient details");
        Console.WriteLine("Send email to patient");

        // Not implementd yet
        //var patient = await patientsApiClient.GetPatientAsync(message.PatientId);
        Console.WriteLine($"Patient Id: {message.PatientId}");

        var emailContent = $"Your appointment is scheduled for {message.AppointmentDate.ToString()}";
        Console.WriteLine($"Email content: {emailContent}");

        await emailService.SendEmailAsync(
            to: "patientemail@patientapi.com",
            subject: "Appointment Confirmation",
            body: emailContent
        );
    }
}
