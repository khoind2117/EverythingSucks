using Newtonsoft.Json;

namespace EverythingSucks.ViewModels
{
    public class MailchimpResponse
    {
        [JsonProperty("members")]
        public List<Member> Members { get; set; }
    }
}
