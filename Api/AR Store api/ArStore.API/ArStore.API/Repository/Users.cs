using System.Collections.Generic;
using ArStore.API.Db;
using ArStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ArStore.API.Repository
{
    public class Users
    {
        static EfContext db = new EfContext();
        
        public static List<User> Get()
        {
            return db.Users.ToList();
        }

        public static User Get(int id)
        {
            return db.Users.FirstOrDefault(p => p.Id == id);
        }

        public static User AddOrUpdate(User user)
        {
            var set = db.Users.Update(user).Entity;

            db.SaveChanges();
            db.Entry(user).State = EntityState.Detached;
            return set;
        }
    }
}