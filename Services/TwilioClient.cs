using EverythingSucks.Helpers;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Http;

namespace EverythingSucks.Services
{
    public class TwilioClient : ITwilioRestClient
    {
        private readonly ITwilioRestClient _innerClient;
        public TwilioClient(IConfiguration config, System.Net.Http.HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _innerClient = new TwilioRestClient(
                config["Twilio:AccountSid"],
                config["Twilio:AuthToken"],
                httpClient: new SystemNetHttpClient(httpClient)
                );
        }
        public string AccountSid => _innerClient.AccountSid;

        public string Region => _innerClient.Region;

        public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;

        public Response Request(Request request) => _innerClient.Request(request);

        public Task<Response> RequestAsync(Request request) => _innerClient.RequestAsync(request);
    }
}
