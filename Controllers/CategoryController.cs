using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameBlog.Models;
using GameBlog.Repositories;

namespace GameBlog.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        public IActionResult Index()
        {
            var toList = categoryRepository.ListT();
            ViewBag.check=toList;
            return View(toList);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            categoryRepository.AddT(p);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCategory(int id)
        {
            categoryRepository.DeleteT(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var toUpdate = categoryRepository.FindById(id);
            return View(toUpdate);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category p)
        {
            categoryRepository.UpdateT(p);
            return RedirectToAction("Index");
        }
    }
}