using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Restaurant_API.Model.Domain;
using Restaurant_API.Model.DTO.Auth;
using Restaurant_API.Model.DTO.Drink;
using Restaurant_API.Model.DTO.Meal;

namespace Restaurant_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Auth Part
            CreateMap<UpdateRequestDto, IdentityUser>();

            // Meal part
            CreateMap<Meal, MealDto>();
            CreateMap<CreateMealDto, Meal>();
            CreateMap<UpdateMealDto, Meal>();

            // Drink part
            CreateMap<Drink, DrinkDto>();
            CreateMap<CreateDrinkDto, Drink>();
            CreateMap<UpdateDrinkDto, Drink>();


        }
    }
}
