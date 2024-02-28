using FamilyBudget.WebServer.Data.Entities;
using FamilyBudget.WebServer.Data.Extensions;
using FamilyBudget.WebServer.Data.Repositories;
using FamilyBudget.WebServer.Mvc.Models;
using FamilyBudget.WebServer.Mvc.Services.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace FamilyBudget.WebServer.Mvc.Controllers
{
    public class UsersController(IUserRepository userRepository, IRabbitMQService rabbitMQService) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var user = await userRepository.GetUserAsync(userId);

            return View(user.ToModel());
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePurchase()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePurchase(PurchaseViewModel purchaseView)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await userRepository.GetUserAsync(userId);

                var purchase = new Purchase()
                {
                    Name = purchaseView.Name,
                    Price = purchaseView.IsReplenishment? purchaseView.Price : -purchaseView.Price,
                    Date = DateTime.UtcNow
                };

                await userRepository.CreatePurchaseAsync(user, purchase);
                return RedirectToAction("Index");
            }
            else
            {
                return View(purchaseView);
            } 
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GenerateReport()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userRepository.GetUserAsync(userId);
            var userReport = new ReportMessage()
            {
                ReportType = "user",
                EmailReport = $"@{user.Email}",
                UserData = user.ToModel()
            };
            var serializedReport = JsonSerializer.Serialize(userReport);
            rabbitMQService.SendMessage(serializedReport);
            return RedirectToAction("Index");
        }
    }
}
