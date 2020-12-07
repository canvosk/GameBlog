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

namespace GameBlog.Repositories
{
    public class BlogRepository:GenericRepository<Blog>
    {
        BlogContext c = new BlogContext();
        public List<SelectListItem> GetCategoryListForDrop()
        {
            List<SelectListItem> toGet = (from x in c.Category.ToList()
                                            select new SelectListItem
                                            {
                                                Text=x.CategoryName,
                                                Value=x.CategoryID.ToString()
                                            }).ToList();
            return toGet;
        }  
    }
}