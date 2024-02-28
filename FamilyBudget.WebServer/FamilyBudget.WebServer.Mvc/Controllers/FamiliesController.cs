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
    public class FamiliesController(IFamilyRepository familyRepository, IUserRepository userRepository, IRabbitMQService rabbitMQService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var family = await familyRepository.GetByIdAsync(id);

            return View(family.ToFullModel());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserFamilies()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var families = await familyRepository.GetByUserIdAsync(userId);
            
            return View(families.Select(family => family.ToModel()).ToList());
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateFamily() 
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFamily(FamilyViewModel familyView)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await userRepository.GetUserAsync(userId);

                var family = await familyRepository.CreateFamilyAsync(user, familyView.Name);

                return RedirectToAction("Index", new { familyId = family.Id});

            }
            else
            {
                return View();
            }    
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult CreatePurchase(int id)
        {
            ViewData["familyId"] = id;
            return View();
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> CreatePurchase(int id, PurchaseViewModel purchaseView)
        {
            if (ModelState.IsValid)
            {
                var family = await familyRepository.GetByIdAsync(id);
                var userName = User.FindFirst(ClaimTypes.Email).Value;
                var purchase = new Purchase()
                {
                    Date = DateTime.UtcNow,
                    FamilyMemberName = userName,
                    Name = purchaseView.Name,
                    Price = purchaseView.IsReplenishment? Math.Abs(purchaseView.Price) : -Math.Abs(purchaseView.Price),
                };

                await familyRepository.AddPurchaseAsync(family, purchase);
                return RedirectToAction("Index", new {id = id});
            }
            else
            {
                return View(purchaseView);
            }
        }

        [HttpDelete("{id}/member/{memberId}")]
        public async Task<ActionResult> RemoveMember(int id, string memberId)
        {
            if (ModelState.IsValid)
            {
                var family = await familyRepository.GetByIdAsync(id);

                await familyRepository.RemoveMemberAsync(family, memberId);

                return RedirectToAction("Index", new { id = id });

            }
            else
            {
                return RedirectToAction("Index", new { id = id });
            }

        }

        [HttpGet("AddMember/{id}")]
        [Authorize]
        public IActionResult AddMember(int id)
        {
            ViewData["familyId"] = id;
            return View();
        }

        [HttpPost("AddMember/{id}")]
        [Authorize]
        public async Task<IActionResult> AddMember(int id, UserViewModel userView)
        {
            if (ModelState.IsValid)
            {
                var user = await userRepository.GetUserByEmailAsync(userView.Email);
                
                if (user == null)
                {
                    ViewData["familyId"] = id;
                    return View(userView);
                }

                var family = await familyRepository.GetByIdAsync(id);

                await familyRepository.AddMemberAsync(family, user);

                return RedirectToAction("Index", new { id = id });
            }
            else
            {
                ViewData["familyId"] = id;
                return View(userView);
            }
        }

        [HttpGet("report/{id}")]
        [Authorize]
        public async Task<ActionResult> GenerateReport(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userRepository.GetUserAsync(userId);
            var family = await familyRepository.GetByIdAsync(id);
            var userReport = new ReportMessage()
            {
                ReportType = "family",
                EmailReport = $"@{user.Email}",
                FamilyData = family.ToFullModel()
            };
            var serializedReport = JsonSerializer.Serialize(userReport);
            rabbitMQService.SendMessage(serializedReport);
            return RedirectToAction("Index", new {id = id});
        }
    }
}
