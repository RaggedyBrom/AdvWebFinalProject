using Microsoft.EntityFrameworkCore;
using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    /// <summary>
    /// Handles initializing the application's database with test data.
    /// </summary>
    public class DbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly IRecipeRepository _recipeRepo;

        /// <summary>
        /// Default constructor which takes in a database context.
        /// </summary>
        /// <param name="db">The database context that the class will use
        /// to carry out the seeding operation.</param>
        public DbInitializer(ApplicationDbContext db, IRecipeRepository recipeRepo)
        {
            _db = db;
            _recipeRepo = recipeRepo;
        }

        /// <summary>
        /// Seeds the application's database with test data.
        /// </summary>
        /// <returns>A Task representing the result of the operation.</returns>
        public async Task SeedAsync()
        {
            // First delete the database if it exists, then apply any migrations
            await _db.Database.EnsureDeletedAsync();
            await _db.Database.MigrateAsync();

            var pumpkinPie = await _db.Recipes.AddAsync(new Recipe
            {
                Name = "Pumpkin Pie",
                Instructions = "1. Make the pie.\n2. Bake the pie.",
                Description = "A delicious pie, perfect for the holiday season!",
                CookTime = 40,
                PrepTime = 30
            });
            var pumpkinChili = await _db.Recipes.AddAsync(new Recipe
            {
                Name = "Pumpkin Chili",
                Instructions = "1. Put the ingredients in a pot.\n2. Cook until done.\n3. Stir occasionally.",
                Description = "This chili will warm you up on a cold winter's day.",
                CookTime = 90,
                PrepTime = 15
            });
            var pumpkinRavioli = await _db.Recipes.AddAsync(new Recipe
            {
                Name = "Pumpkin Ravioli",
                Instructions = "1. Form the ravioli.\n2. Put them in the oven.",
                Description = "Something new to try when regular old ravioli have become boring for you.",
                CookTime = 20,
                PrepTime = 45
            });

            var pumpkin = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Pumpkin Puree",
                Type = IngredientType.Vegetable
            });
            var flour = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Flour",
                Type = IngredientType.Grain
            });
            var sugar = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Sugar",
                Type = IngredientType.Spice
            });
            var water = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Water",
                Type = IngredientType.Seasoning
            });
            var hamburger = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Hamburger Meat",
                Type = IngredientType.Meat
            });
            var kidneyBeans = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Kidney Beans",
                Type = IngredientType.Bean
            });
            var ricottaCheese = await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "Ricotta Cheese",
                Type = IngredientType.Dairy
            });

            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinPie.Entity,
                Ingredient = pumpkin.Entity,
                Amount = "1 cup",
                Calories = 300
            });
            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinPie.Entity,
                Ingredient = flour.Entity,
                Amount = "2 cups",
                Calories = 500
            });
            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinPie.Entity,
                Ingredient = water.Entity,
                Amount = "240 mL",
                Calories = 0
            });
            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinPie.Entity,
                Ingredient = sugar.Entity,
                Amount = "3 tbsp",
                Calories = 150
            });

            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinChili.Entity,
                Ingredient = pumpkin.Entity,
                Amount = "2 cups",
                Calories = 300
            });
            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinChili.Entity,
                Ingredient = hamburger.Entity,
                Amount = "8 oz",
                Calories = 400
            });
            await _db.RecipeIngredients.AddAsync(new RecipeIngredient
            {
                Recipe = pumpkinChili.Entity,
                Ingredient = pumpkin.Entity,
                Amount = "2 cups",
                Calories = 300
            });


            await _db.SaveChangesAsync();
        }
    }
}
