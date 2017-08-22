using InMemDb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemDb.Data
{
    public static class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser();
            aUser.UserName = "student@test.com";
            aUser.Email = "student@test.com";
            var r = userManager.CreateAsync(aUser, "Pass!234").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;
                
            var adminUser = new ApplicationUser();
            adminUser.UserName = "admin@test.com";
            adminUser.Email = "admin@test.com";
            var adminUserResult = userManager.CreateAsync(adminUser, "Pass!234").Result;

            userManager.AddToRoleAsync(adminUser, "Admin");

            if(context.Dishes.ToList().Count == 0)
            {
                var cheese = new Ingredient { Name = "Cheese" };
                var tomatoe = new Ingredient { Name = "Tomatoe" };
                var ham = new Ingredient { Name = "Ham" };
                var gorgonzola = new Ingredient { Name = "Gorgonzola" };
                var mozzarella = new Ingredient { Name = "Mozzarella" };
                var goatCheese = new Ingredient { Name = "GoatCheese" };

                var capricciosa = new Dish { Name = "Capricciosa", Price=79 };
                var margaritha = new Dish { Name = "Margaritha", Price = 69 };
                var hawaii = new Dish { Name = "Hawaii", Price = 85 };
                var quattroFormaggio = new Dish { Name = "QuattroFormaggio", Price = 95 };

                var quattroFormaggioGorgonzola = new DishIngredient { Dish = quattroFormaggio, Ingredient = gorgonzola };
                var quattroFormaggioMozzarella = new DishIngredient { Dish = quattroFormaggio, Ingredient = mozzarella };
                var quattroFormaggioGoatCheese = new DishIngredient { Dish = quattroFormaggio, Ingredient = goatCheese };
                quattroFormaggio.DishIngredients = new List<DishIngredient>();
                quattroFormaggio.DishIngredients.Add(quattroFormaggioGorgonzola);
                quattroFormaggio.DishIngredients.Add(quattroFormaggioGoatCheese);
                quattroFormaggio.DishIngredients.Add(quattroFormaggioMozzarella);

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaTomatoe = new DishIngredient { Dish = capricciosa, Ingredient = tomatoe };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaCheese);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaTomatoe);

                context.Dishes.Add(capricciosa);
                context.Dishes.Add(margaritha);
                context.Dishes.Add(hawaii);
                context.AddRange(tomatoe, ham, cheese, gorgonzola, mozzarella, goatCheese);

                //context.AddRange(capricciosa, margaritha, hawaii);
                context.SaveChanges();
            }
        }
    }
}
