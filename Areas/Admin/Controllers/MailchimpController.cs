using CsvHelper.Configuration;
using EverythingSucks.Data;
using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Net.Http.Headers;
using System.Linq;


namespace EverythingSucks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MailchimpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MailchimpController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<List<Subscriber>> GetSubscribersFromMailchimpAsync()
        {
            var apiKey = "ab522933259cd61787a4c8ea2b28104d-us22";
            var listId = "d813a3d330";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await client.GetAsync($"https://us22.api.mailchimp.com/3.0/lists/{listId}/members");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse); // In JSON để kiểm tra

                var mailchimpResponse = JsonConvert.DeserializeObject<MailchimpResponse>(jsonResponse);

                return mailchimpResponse?.Members
                    .Select(m => new Subscriber
                    {
                        EmailAddress = m?.EmailAddress ?? ""
                    })
                    .ToList() ?? new List<Subscriber>();
            }

            // Ném ngoại lệ nếu có lỗi
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to retrieve subscribers from Mailchimp. Status Code: {response.StatusCode}. Error: {errorContent}");
        }


        public void ExportSubscribersToCsv(List<Subscriber> subscribers, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(subscribers);
            }
        }

        public async Task<IActionResult> ExportSubscribers()
        {
            var subscribers = await GetSubscribersFromMailchimpAsync();

            // Tạo tệp CSV trong thư mục tạm thời
            var filePath = Path.Combine(Path.GetTempPath(), "subscribers.csv");
            ExportSubscribersToCsv(subscribers, filePath);

            // Trả về tệp CSV cho người dùng tải xuống
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "text/csv", "subscribers.csv");
        }
    }
}
