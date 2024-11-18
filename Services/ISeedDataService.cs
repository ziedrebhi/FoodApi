using FoodApi.Repositories;

namespace FoodApi.Services
{
    public interface ISeedDataService
    {
        void Initialize(FoodDbContext context);
    }
}
