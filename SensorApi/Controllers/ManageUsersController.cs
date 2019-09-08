using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SensorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SensorApi.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser>
            _userManager;

        public ManageUsersController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager
                .GetUsersInRoleAsync(Constants.AdministratorRole))
                .ToArray();
            var users = (await _userManager
                .GetUsersInRoleAsync(Constants.UserRole))
                .ToArray();
            var devices = (await _userManager
                .GetUsersInRoleAsync(Constants.DeviceRole))
                .ToArray();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Users = users,
                Devices = devices
            };

            return View(model);
        }
    }
}
