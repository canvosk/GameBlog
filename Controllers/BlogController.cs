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
using Microsoft.AspNetCore.Http;
using System.IO;

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
        public async Task<IActionResult> AddBlog(Blog p,IFormFile file)
        {
            var cat = c.Category.Where(x => x.CategoryID == p.Category.CategoryID).FirstOrDefault();
            p.Category = cat;
            DateTime date = DateTime.Now;
            p.BlogDate = date;
            p.Counter = 0;

            if(file!=null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                p.ImagePath=randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

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
        public async Task<IActionResult> UpdateBlog(Blog p, IFormFile file)
        {
            var cat = c.Category.Where(x => x.CategoryID == p.Category.CategoryID).FirstOrDefault();
            p.Category = cat;

            if(file!=null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                p.ImagePath=randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            
            blogRepository.UpdateT(p);
            return RedirectToAction("Index");
        }
    }
}