using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CommonApiModel
    {
        public string Message { get; set; }
    }
    public static class HttpClientExtensions
    {
        public static async Task<T> GetApiResponse<T>(this HttpClient client, HttpMethod method, string url, object parameters)
        {
            var request = new HttpRequestMessage(method, url);

            var content = JsonConvert.SerializeObject(parameters);

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("send http failed.");

            return JsonConvert.DeserializeObject<T>(str);
        }
    }

    public class ReportService
    {
        public void SendReport()
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var response = client.GetApiResponse<CommonApiModel>(HttpMethod.Post, "http://www.api-report.net/", new
                    {
                        OrderNo = "A20220924001",
                        Status = "Completed"
                    });
                }
            }
            catch (Exception)
            {
                // implement logging
            }
        }
    }

    public class Unit7Http
    {
        [Fact]
        public void SendHttp()
        {
            // Arrange

            // Act

            // Assert
        }


        //public async Task SendReport(CommonApiModel param)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.Timeout = TimeSpan.FromSeconds(200);

        //        var request = new HttpRequestMessage(/*HttpMethod*/, /*url*/);

        //        request.Content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

        //        var response = await client.SendAsync(request);

        //        if(response.IsSuccessStatusCode)
        //        {
        //            // implement logic success case
        //        }
        //    }
        //}
    }
}
