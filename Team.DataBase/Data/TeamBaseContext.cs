
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Team.Models;

namespace Team.DataBase.Data
{
    public class TeamBaseContext : IdentityDbContext
    {
        public TeamBaseContext(DbContextOptions<TeamBaseContext> options) : base(options)
        {

        }

        public DbSet<Category> Category1 { get; set; }

        public DbSet<FurnitureTypes> FurnitureTypes { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}
