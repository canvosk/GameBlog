using System.Collections.Generic;

namespace GameBlog.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}