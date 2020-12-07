using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameBlog.Models;
using GameBlog.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameBlog.Controllers
{
    public class BlogController : Controller
    {
        BlogRepository blogRepository = new BlogRepository();
        BlogContext c = new BlogContext();

        public IActionResult Index()
        {
            var toList = blogRepository.ListT();
            ViewBag.check=toList;
            var toListWithCategory = c.Blogs.Include(x => x.Category).ToList();
            return View(toListWithCategory);
        }
        [HttpGet]
        public IActionResult AddBlog()
        {
            var toGet = blogRepository.GetCategoryListForDrop();
            ViewBag.values = toGet;
            return View();
        }
        [HttpPost]
        public IActionResult AddBlog(Blog p)
        {
            var cat = c.Category.Where(x => x.CategoryID == p.Category.CategoryID).FirstOrDefault();
            p.Category = cat;
            DateTime date = DateTime.Now;
            p.BlogDate = date;
            p.Counter = 0;
            c.Blogs.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteBlog(int id)
        {
            blogRepository.DeleteT(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateBlog(int id)
        {
            var val = blogRepository.GetCategoryListForDrop();
            ViewBag.values = val;
            var toGet = blogRepository.FindById(id);
            return View(toGet);
        }
        [HttpPost]
        public IActionResult UpdateBlog(Blog p)
        {
            var cat = c.Category.Where(x => x.CategoryID == p.Category.CategoryID).FirstOrDefault();
            p.Category = cat;
            blogRepository.UpdateT(p);
            return RedirectToAction("Index");
        }
    }
}