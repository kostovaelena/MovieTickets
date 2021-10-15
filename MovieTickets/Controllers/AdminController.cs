using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTickets.Domain;
using MovieTickets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
    
    public class AdminController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IOrderService productService;
        private readonly UserManager<MTApplicationUser> userManager;

        public AdminController(IOrderService orderService, IOrderService productService, UserManager<MTApplicationUser> userManager)
        {
            this._orderService = orderService;
            this.userManager = userManager;
            this.productService = productService;
        }

        
        public List<Order> GetOrders()
        {
            return this._orderService.getAllOrders();
        }

        public List<Movie> GetMovies()
        {
            return this.productService.GetAllProducts(); 
        }


        [HttpPost("[action]")]
        public Order GetDetailsForProduct(BaseEntity model)
        {
            return this._orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDto> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new MTApplicationUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new ShoppingCart(),
                        
                    };
                    var result = userManager.CreateAsync(user, item.Password).Result;

                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }






      
    }
}
