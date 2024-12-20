﻿using Microsoft.AspNetCore.Mvc;
using FoodApi.Models;

namespace FoodApi.Services
{
    public interface ILinkService<T>
    {
        object ExpandSingleFoodItem(object resource, int identifier, ApiVersion version);

        List<LinkDto> CreateLinksForCollection(QueryParameters queryParameters, int totalCount, ApiVersion version);
    }
}
