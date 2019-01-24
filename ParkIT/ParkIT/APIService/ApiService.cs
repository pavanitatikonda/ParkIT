using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkIT.APIService
{
    public class ApiService
    {
        private string _URL;
        
        private string _token;

        HttpClient client;


        public ApiService(string URL)
        {
            client = new HttpClient();
            
            _URL = URL;
        }

        private HttpClient CreateHttpClient()
        {
            try
            {
                client.BaseAddress = new Uri(_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T> Get<T>(string route)
        {

            try
            {
                HttpClient client = CreateHttpClient();
                using (client)
                {
                    HttpResponseMessage response = await client.GetAsync(route);

                    if (response.IsSuccessStatusCode)
                    {
                       var result = await response.Content.ReadAsStringAsync();

                        var x = JsonConvert.DeserializeObject<T>(result);
                        return x;
                    }
                    else
                    {
                        var message = await response.Content.ReadAsStringAsync();

                        throw new Exception(message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<U> Post<T, U>(T data, string route, string contentType = "application/json")
        {
            try
            {
                StringContent content = null;

                if (data != null)
                {
                    string jsonData = data is string ? data.ToString() : JsonConvert.SerializeObject(data);
                    content = new StringContent(jsonData, Encoding.UTF8, contentType);
                }

                HttpClient client = CreateHttpClient();

                using (client)
                {
                    HttpResponseMessage response = await client.PostAsync(route, data != null ? content : null);

                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<U>(res);
                    }
                    else
                    {
                        var message = await response.Content.ReadAsStringAsync();

                        throw new Exception(message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static bool CheckTokenExpiry()
        {
            if (App.Current.Properties.ContainsKey("Login_Time") && App.Current.Properties.ContainsKey("ExpiryInMinutes"))
            {
                if (DateTime.Now > Convert.ToDateTime(App.Current.Properties["Login_Time"]).AddSeconds(Convert.ToInt16(App.Current.Properties["ExpiryInMinutes"])))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

