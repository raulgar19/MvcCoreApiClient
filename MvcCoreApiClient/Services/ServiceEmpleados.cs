using MvcCoreApiClient.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Services
{
    public class ServiceEmpleados
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiEmpleados");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync("api/empleados");
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }
                else
                {
                    throw new Exception("Error en la api " + (int)response.StatusCode);
                }
            }
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/empleados/" + idEmpleado;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Empleado empleado = await response.Content.ReadAsAsync<Empleado>();
                    return empleado;
                }
                else
                {
                    throw new Exception("Error en la api" + (int)response.StatusCode);
                }
            }
        }
    }
}