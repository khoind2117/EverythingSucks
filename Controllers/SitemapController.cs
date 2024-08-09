using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;
using System.IO;
using EverythingSucks.Data;
using EverythingSucks.Services;

namespace YourNamespace.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SitemapController(ApplicationDbContext context,
            PhotoService photoService)
        {
            _context = context;
        }

        [Route("sitemap.xml")]
        public IActionResult Sitemap()
        {
            // Create an XML document
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XDocument sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset")
            );

            // Base URL
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            // Get all products from the database
            var products = _context.Products.Select(p => p.Id).ToList();

            // Generate URLs for each product
            foreach (var productId in products)
            {
                string productUrl = $"{baseUrl}/vi/product/detail?productId={productId}";

                sitemap.Root.Add(
                    new XElement(ns + "url",
                        new XElement(ns + "loc", productUrl),
                        new XElement(ns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                        new XElement(ns + "changefreq", "weekly"),
                        new XElement(ns + "priority", "0.8")
                    )
                );
            }

            // Use a StringWriter to include the XML declaration
            using (var stringWriter = new StringWriter())
            {
                sitemap.Save(stringWriter);
                var xml = stringWriter.ToString();
                return Content(xml, "application/xml", Encoding.UTF8);
            }
        }
    }
}
