using Microsoft.EntityFrameworkCore;

namespace GameBlog.Models
{
    public class BlogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=GameBlogDB;integrated security=true;");
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}