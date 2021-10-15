using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MovieTickets.Domain;

using MovieTickets.Services.Interface;


namespace MovieTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IProductService _productService;
        

      


        public MoviesController(IProductService productServicet)
        {
            _productService = productServicet;
            
        }


        public IActionResult AddToShoppingCart(Guid? id)
        {
            var model = this._productService.GetShoppingCartInfo(id);
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToShoppingCart([Bind("MovieId", "Quantity")] AddToShoppingCart item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._productService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Movies");
            }

            return View(item);
        }





        // GET: Movies
        public IActionResult Index(string sortOrder)
        {
            
            var allProducts = this._productService.GetAllProducts();
            return View(allProducts);
        }

       
       
        

        // GET: Movies/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._productService.GetDetailsForProduct(id);
               
            this._productService.GetDetailsForProduct(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        // GET: Movies/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([Bind("Id,Image,Name,Description,Traenje,Zemja,Godina,Zanr,Akteri,Kategorija,Rating,Price,Datum,Vreme")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                this._productService.CreateNewProduct(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }


        // GET: Movies/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._productService.GetDetailsForProduct(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Guid id, [Bind("Id,Image,Name,Description,Traenje,Zemja,Godina,Zanr,Akteri,Kategorija,Rating,Price,Datum,Vreme")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._productService.UpdeteExistingProduct(movie);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }


        // GET: Movies/Delete/5
        [Authorize(Roles = "Administrator")]
        public  IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._productService.GetDetailsForProduct(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
            return this._productService.GetDetailsForProduct(id) != null;
        }





        /*
                [HttpGet]
                public FileContentResult ExportMovies()
                {
                    string fileName = "Movies.xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Movies");

                        worksheet.Cell(1, 1).Value = "Movie Id";


                        *//* HttpClient client = new HttpClient();


                         string URI = "https://localhost:44336/Admin/GetMovies";

                         HttpResponseMessage responseMessage = client.GetAsync(URI).Result;

                         var result = responseMessage.Content.ReadAsAsync<List<Order>>().Result;*//*

                        for (int i = 1; i <= result.Count(); i++)
                        {
                            var item = result[i - 1];

                            worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                            worksheet.Cell(i + 1, 2).Value = item.User.Email;

                            for (int p = 0; p < item.MovieInOrders.Count(); p++)
                            {
                                worksheet.Cell(1, p + 3).Value = "Movie-" + (p + 1);
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
                }*/


        public IActionResult FilterByGenre(String genre)
        {
            Zanr genreEnum = (Zanr)Enum.Parse(typeof(Zanr), genre);

            var allTickets = this._productService.FilterByGenre(genreEnum);

            return View("Index", allTickets);
        }
        private bool TicketExists(Guid id)
        {
            return this._productService.GetDetailsForProduct(id) != null;
        }


        public IActionResult FilteredTickets(DateTime date)
        {
            var allTickets = this._productService.GetFilteredTickets();
            return View(allTickets);
        }


        

    }
}
