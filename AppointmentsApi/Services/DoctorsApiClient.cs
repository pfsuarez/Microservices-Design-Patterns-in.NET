using AppointmentsApi.Models;

namespace AppointmentsApi.Services;

public class DoctorsApiClient(HttpClient httpClient)
{
    public async Task<Doctors> GetDoctorAsync(Guid doctorId)
    {
        var response = await httpClient.GetAsync($"/api/doctors/{doctorId}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Doctors>() ?? null!;
    }   
}
