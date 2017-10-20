using Dao;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Utils
{
    public class Initialize
    {
        public static void DbInit(DwDbContext c)
        {
            c.Database.EnsureCreated();
            //c.Database.Migrate();
            c.SaveChanges();
        }
    }
}