using System.Collections.Generic;
using System.Linq;
using GameBlog.Models;

namespace GameBlog.Repositories
{
    public class GenericRepository<T> where T:class,new() 
    {
        BlogContext c = new BlogContext();

        public List<T> ListT()
        {
            return c.Set<T>().ToList();
        }

        public void AddT(T p)
        {
            c.Set<T>().Add(p);
            c.SaveChanges();
        }
        public T FindById(int id)
        {
            return c.Set<T>().Find(id);
        }
        public void DeleteT(int id)
        {
            var toDelete = c.Set<T>().Find(id);
            c.Set<T>().Remove(toDelete);
            c.SaveChanges();
        }
        public void UpdateT(T p)
        {
            c.Set<T>().Update(p);
            c.SaveChanges();
        }
    }
}