using Newtonsoft.Json;

namespace EverythingSucks.ViewModels
{
    public class Member
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
    }
}
