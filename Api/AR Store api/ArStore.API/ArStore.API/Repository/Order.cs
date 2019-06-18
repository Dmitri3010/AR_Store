using System.Collections.Generic;
using System.Linq;
using ArStore.API.Db;
using ArStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ArStore.API.Repository
{
    public static class Orders
    {
        static EfContext db = new EfContext();

        public static List<Order> Get()
        {
            return db.Orders.ToList();
        }

        public static Order Get(int id)
        {
            return db.Orders.FirstOrDefault(p => p.Id == id);
        }

        public static Order AddOrUpdate(Order order)
        {
            var set = db.Orders.Update(order).Entity;

            db.SaveChanges();
            db.Entry(order).State = EntityState.Detached;
            return set;
        }
    }
}