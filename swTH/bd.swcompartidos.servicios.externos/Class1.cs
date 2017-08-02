using bd.swth.entidades.EntidadesServicios;
using bd.swth.entidades.Utils;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bd.sw.externos
{
    public   class Class1
    {
        public  Class1()
        {
           

        }

        public async Task<Response> EjemploAsync(Pais paisObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(paisObject);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    client.BaseAddress = new Uri("http://localhost:53317");
                    var url = "/api/Paises";
                    var response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Errro",
                        };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var pais = JsonConvert.DeserializeObject<Pais>(result);

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Login Ok",
                        Resultado = pais,
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }

        }

    }
}
