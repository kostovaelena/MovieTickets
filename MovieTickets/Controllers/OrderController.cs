//using ClosedXML.Excel;
using MovieTickets.Domain;
//using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieTickets.Services.Interface;
using GemBox.Document;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;

namespace MovieTickets.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        }

        


        public IActionResult Index()
        {
            HttpClient client = new HttpClient();


            string URI = "https://localhost:44336/Admin/GetOrders";

            HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

            var result = responseMessage.Content.ReadAsAsync<List<Order>>().Result;

            return View(result);
        }

        public IActionResult Details(Guid id)
        {
            /* HttpClient client = new HttpClient();


             string URI = "https://localhost:44336/Admin/GetDetailsForProduct";

             var model = new
             {
                 Id = id
             };

             HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

             HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;


             var result = responseMessage.Content.ReadAsAsync<Order>().Result;*/


            BaseEntity baseEntity = new BaseEntity();
            baseEntity.Id = id;

            Order result = this._orderService.getOrderDetails(baseEntity);


            return View(result);
        }
       
        [HttpGet]
        public FileContentResult CreateInvoice(Guid id)
        {
            /* HttpClient client = new HttpClient();


             string URI = "https://localhost:44336/Admin/GetDetailsForProduct";

             var model = new
             {
                 Id = id
             };

             HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

             HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;


             var result = responseMessage.Content.ReadAsAsync<Order>().Result;


 */


            BaseEntity baseEntity = new BaseEntity();
            baseEntity.Id = id;

            Order result = this._orderService.getOrderDetails(baseEntity);


            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);


            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", result.User.UserName);

            StringBuilder sb = new StringBuilder();

            var totalPrice = 0;

            foreach (var item in result.MovieInOrders)
            {
                totalPrice += item.Quantity * item.OrderedMovie.Price;
                sb.AppendLine(item.OrderedMovie.Name + " with quantity of: " + item.Quantity + " and price of: " + item.OrderedMovie.Price + "$");
            }


            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", totalPrice.ToString() + "$");


            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public FileContentResult ExportAllOrders()
        {
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Orders");

                worksheet.Cell(1, 1).Value = "Order Id";
                worksheet.Cell(1, 2).Value = "Costumer Email";
                


                HttpClient client = new HttpClient();


                string URI = "https://localhost:44336/Admin/GetOrders";

                HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

                var result = responseMessage.Content.ReadAsAsync<List<Order>>().Result;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.User.Email;

                    for (int p = 0; p < item.MovieInOrders.Count(); p++)
                    {
                        worksheet.Cell(1, p + 3).Value = "Product-" + (p + 1);
                        worksheet.Cell(i + 1, p + 3).Value = item.MovieInOrders.ElementAt(p).OrderedMovie.Name;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }

    }
}
