using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serene.DataAccess.Repository.IRepositories;
using Serene.InterfacesAndRepos;
using Serene.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Serene.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> GetAllData()
        {
            var data = await categoryRepo.GetAll();
            return View(data);
        }

        public async Task<IActionResult> GetData(int id)
        {
            var data = categoryRepo.Get(u=>u.CategoryId == id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                categoryRepo.Create(model);
                categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("GetAllData");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var data = categoryRepo.Get(u=>u.CategoryId == id);
            if (data != null)
                return View(data);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {

            var data = categoryRepo.Get(u=>u.CategoryId == model.CategoryId);
            if (data != null)
            {
                categoryRepo.Update(model);
                categoryRepo.Save();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction(nameof(GetAllData));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = categoryRepo.Get(u => u.CategoryId == id);
            categoryRepo.Remove(data);
            categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(GetAllData));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
