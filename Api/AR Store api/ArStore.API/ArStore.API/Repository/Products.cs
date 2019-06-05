using ArStore.API.Db;
using ArStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ArStore.API.Repository
{
    public class Products
    {
        static EfContext db = new EfContext();

        public static List<Product> Get()
        {
            return db.Products.ToList();
        }

        public static Product Get(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        public static Product AddOrUpdate(Product product)
        {
            var set = db.Products.Update(product).Entity;

            db.SaveChanges();
            db.Entry(product).State = EntityState.Detached;
            return set;
        }

        public static Product Delete(Product product)
        {
            var set = db.Products.Remove(product).Entity;

            db.SaveChanges();

            return set;
        }
    }
}
