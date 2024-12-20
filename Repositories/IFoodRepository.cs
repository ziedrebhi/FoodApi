﻿using FoodApi.Entities;
using FoodApi.Models;

namespace FoodApi.Repositories
{
    public interface IFoodRepository
    {
        FoodEntity GetSingle(int id);
        void Add(FoodEntity item);
        void Delete(int id);
        FoodEntity Update(int id, FoodEntity item);
        IQueryable<FoodEntity> GetAll(QueryParameters queryParameters);
        ICollection<FoodEntity> GetRandomMeal();
        int Count();
        bool Save();
    }
}
