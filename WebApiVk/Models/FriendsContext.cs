using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiVk.Models
{
    public class FriendsContext:DbContext
    {
        public DbSet<Friend> Friends
        {
            get; set;
        }

        public FriendsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=VkBirthday;Trusted_Connection=True;");
        }

    }
}
