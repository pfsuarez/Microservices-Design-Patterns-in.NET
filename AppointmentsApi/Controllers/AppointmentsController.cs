using AppointmentsApi.Models;
using AppointmentsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentContext _context;
        private readonly PatientsApiClient _patientsApiClient;
        private readonly DoctorsApiClient _doctorsApiClient;

        public AppointmentsController(
            AppointmentContext context,
            PatientsApiClient patientsApiClient,
            DoctorsApiClient doctorsApiClient
        )
        {
            _context = context;
            _patientsApiClient = patientsApiClient;
            _doctorsApiClient = doctorsApiClient;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            // Synchronously call api patients and doctors to get details.

            // var patient = await _patientsApiClient.GetPatientAsync(appointment.PatientId);
            // var doctor = await _doctorsApiClient.GetDoctorAsync(appointment.DoctorId);

            // AppointmetDetails appointmetDetails = new AppointmetDetails(
            //     id,
            //     patient,
            //     doctor,
            //     appointment.Slot.Start,
            //     appointment.Slot.End,
            //     appointment.Location.RoomNumber,
            //     appointment.Location.Building
            // );
            // return Ok(appointmetDetails);

            return appointment;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(Guid id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetAppointment",
                new { id = appointment.AppointmentId },
                appointment
            );
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(Guid id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
