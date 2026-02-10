using AppointmentsApi.Consumer;
using AppointmentsApi.Models;
using AppointmentsApi.Services;
using Google.Protobuf.WellKnownTypes;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppointmentContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpClient<PatientsApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiEndpoints:PatientsApi"]!);
});

builder.Services.AddHttpClient<DoctorsApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiEndpoints:DoctorsApi"]!);
});

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<AppointmentCreatedConsumer>();
    options.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.ReceiveEndpoint(
                "appointment-created-queue",
                e =>
                {
                    e.PrefetchCount = 1;
                    e.UseConcurrencyLimit(1);
                    e.ConfigureConsumer<AppointmentCreatedConsumer>(context);
                }
            );
        }
    );
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
