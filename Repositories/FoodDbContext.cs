using Microsoft.EntityFrameworkCore;
using FoodApi.Entities;

namespace FoodApi.Repositories
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodEntity> FoodItems { get; set; } = null!;
    }
}
