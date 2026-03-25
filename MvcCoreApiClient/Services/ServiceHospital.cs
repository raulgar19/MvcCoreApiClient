using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Services
{
    public class ServiceHospital
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceHospital(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiHospitales");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync("api/hospitales");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    List<Hospital> hospitales = JsonConvert.DeserializeObject<List<Hospital>>(json);
                    return hospitales;
                }
                else
                {
                    throw new Exception("Error en la api " + (int)response.StatusCode);
                }
            }
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/hospitales/" + idHospital;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Hospital hospital = await response.Content.ReadAsAsync<Hospital>();
                    return hospital;
                }
                else
                {
                    throw new Exception("Error en la api" + (int)response.StatusCode);
                }
            }
        }
    }
}