using System.Collections.Concurrent;
using AppointmentsApi.Models.Messages;
using AppointmentsApi.Services;
using MassTransit;

namespace AppointmentsApi.Consumer;

public class AppointmentCreatedConsumer(
    PatientsApiClient patientsApiClient,
    DoctorsApiClient doctorsApiClient,
    IEmailService emailService
) : IConsumer<AppointmentCreated>
{
    private static readonly ConcurrentDictionary<Guid, DateTime> LastProcessedMessages = new();
    private static readonly ConcurrentDictionary<Guid, bool> ProcessedMessageIds = new();

    public async Task Consume(ConsumeContext<AppointmentCreated> context)
    {
        var message = context.Message;

        if (ProcessedMessageIds.ContainsKey(message.MessageId))
        {
            Console.WriteLine(
                $"Duplicate message detected for MessageId: {message.MessageId}. Skipping processing."
            );
            return;
        }

        var lastTimestamp = LastProcessedMessages.GetOrAdd(
            message.AppointmentId,
            message.Timestamp
        );

        if (message.Timestamp > lastTimestamp)
        {
            LastProcessedMessages[message.AppointmentId] = message.Timestamp;
        }
        else
        {
            Console.WriteLine(
                $"Implement logic to handle out-of-order message for AppointmentId: {message.AppointmentId}."
            );
        }

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

        var emailContent =
            $"Your appointment is scheduled for {message.AppointmentDate.ToString()}";
        Console.WriteLine($"Email content: {emailContent}");

        await emailService.SendEmailAsync(
            to: "patientemail@patientapi.com",
            subject: "Appointment Confirmation",
            body: emailContent
        );

        ProcessedMessageIds[message.MessageId] = true;
    }
}
