using ArStore.API.Db;
using ArStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ArStore.API.Repository
{
    public class Categories
    {
        static EfContext db = new EfContext();

        public static List<Category> Get()
        {
            return db.Categories.ToList();
        }

        public static Category Get(int id)
        {
            return db.Categories.FirstOrDefault(p => p.Id == id);
        }

        public static Category AddOrUpdate(Category category)
        {
            var set = db.Categories.Update(category).Entity;

            db.SaveChanges();
            db.Entry(category).State = EntityState.Detached;
            return set;
        }

        public static Category Delete(Category category)
        {
            var set = db.Categories.Remove(category).Entity;

            db.SaveChanges();

            return set;
        }
    }
}
