using Demo.Models;
using Demo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class PetsController:Controller
    {
        private readonly IPetRepository _repo;

        public PetsController(IPetRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()=>View(_repo.GetAllPets());
        public IActionResult Details(int id){
            var pet = _repo.GetSinglePet(id);
            if (pet==null)
            {
                return NotFound();
            }
            return View(pet);
        }
       
       [HttpGet]
        public IActionResult New(){
           // ViewBag.IsEditMode="false";
            var pet=new Pet();
            return View(pet);
        }
        [HttpPost]
        public IActionResult New(Pet pet){
                   _repo.Create(pet);

            /*if(isEditMode.Equals("false")){
            _repo.Create(pet);
            }
            else if(isEditMode.Equals("true")){
                _repo.Edit(pet);
                
        }*/
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id){
           // ViewBag.IsEditMode="true";
            var pet=_repo.GetSinglePet(id);

            if(pet==null){
                return NotFound();
            }

            return View(pet);
        }
  [HttpPost]
  public IActionResult Edit(int id,Pet pet){
      if (pet.Id !=id)
      {
          return NotFound();
      }
      _repo.Edit(pet);
      return RedirectToAction(nameof(Index));
  }
        [HttpPost]
        public IActionResult Delete(int id){
            Pet pet=_repo.GetSinglePet(id);
            if (pet==null)
            {
                return NotFound();
            }
            _repo.Delete(pet);
            return RedirectToAction(nameof(Index));
        }
    }
}