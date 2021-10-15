using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTickets.Domain;


namespace MovieTickets.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<MTApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<MTApplicationUser> userManager)
        {
            _roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRole userRole)
        {
            var roleExist = await this._roleManager.RoleExistsAsync(userRole.RoleName);
            if(!roleExist)
            {
                var result = await this._roleManager.CreateAsync(new IdentityRole(userRole.RoleName));
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult AddUserToRole()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(String email, String role)
        {
            var user = await this.userManager.FindByNameAsync(email);
            await this.userManager.AddToRoleAsync(user, role);
            return View();
        }
    }
}
