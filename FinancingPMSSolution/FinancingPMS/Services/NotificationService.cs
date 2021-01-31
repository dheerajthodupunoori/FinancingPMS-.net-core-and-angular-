using FinancingPMS.Interfaces;
using FinancingPMS.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FinancingPMS.Services
{
    public class NotificationService : INotificationService
    {

        private  HttpClient httpClient;

        private IHttpClientFactory _httpClientFactory;

        public NotificationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public string SendNotification(NotificationDetails notificationDetails)
        {
            httpClient = _httpClientFactory.CreateClient("NotificationClient");

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post , "/SendEmailNotification");
            var stringContent = new StringContent(JsonConvert.SerializeObject(notificationDetails) , Encoding.UTF8 , "application/json");
            httpRequestMessage.Content = stringContent;

            var response = httpClient.SendAsync(httpRequestMessage);

            return response.Result.Content.ReadAsStringAsync().Result.ToString();
        }
    }
}
