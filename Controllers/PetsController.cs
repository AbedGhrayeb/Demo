using System;
using System.Linq;
using ClientNotifications;
using Demo.Models;
using Demo.Repositories;
using Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static ClientNotifications.Helpers.NotificationHelper;

namespace Demo.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetRepository _repo;
        private readonly UserManager<AppUser> _userManager;

        public PetsController(IPetRepository repo,UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        public IActionResult Index(string search=null)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var foundPets = _repo.SearchPets(search);
                return View(foundPets);
            }
            return View(_repo.GetAllPets());
        }
        public IActionResult MyPets()
        {
            var pets = _repo.GetPetsByUserId(GetUserId());
            return View(pets);
        }
        public IActionResult Details(int id)
        {
            var pet = _repo.GetSinglePet(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }
        [Authorize]
        [HttpGet]
        public IActionResult New()
        {
            var pet = new NewPetVM();
            return View(pet);
        }
        [Authorize]
        [HttpPost]
        public IActionResult New(NewPetVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Pet pet = new Pet
            {
                Name = model.Name,
                Age = model.Age,
                Color = model.Color,
                UserId = GetUserId()
            };
                _repo.Create(pet);

                return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var pet = _repo.GetSinglePet(id);

            if (pet == null)
            {
                return NotFound();
            }
            if (pet.UserId!=GetUserId())
            {
                return RedirectToAction("AccessDenied","Account");
            }
            var model = new EditPetVM
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Color = pet.Color

            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(EditPetVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var pet = _repo.GetSinglePet(model.Id);
            pet.Name = model.Name ?? pet.Name;
            pet.Age = model.Age;
            pet.Color = model.Color ?? pet.Color;

            _repo.Edit(pet);
                return RedirectToAction(nameof(Index));
          
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Pet pet = _repo.GetSinglePet(id);
            if (pet == null)
            {
                return NotFound();
            }
            if (pet.UserId != GetUserId())
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            _repo.Delete(pet);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult VerifyName(string name)
        {
            if (_repo.VerifyName(name))
            {
                return Json("this name already taken");
            }
            return Json(true);
        }
        public IActionResult AutocompleteResult(string search)
        {
            return Json(_repo.SearchPets(search));
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(this.HttpContext.User);
        }
    }
}