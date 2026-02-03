using AppointmentsApi.Models;

namespace AppointmentsApi.Services;

public class PatientsApiClient(HttpClient httpClient)
{
    public async Task<Patient> GetPatientAsync(Guid patientId)
    {
        var response = await httpClient.GetAsync($"/api/patients/{patientId}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Patient>() ?? null!;
    }
}
