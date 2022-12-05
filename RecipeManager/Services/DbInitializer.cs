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
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis at lectus non mauris euismod commodo eget non nunc. Quisque vitae neque vitae lorem volutpat tristique. Donec semper dignissim arcu, nec bibendum libero tempus at. Vivamus tristique metus condimentum nisl tempus, vel consequat libero hendrerit. Aenean sit amet ligula efficitur, faucibus elit vitae, rhoncus erat. Pellentesque ut dui eu magna porta sodales vitae eu orci. Morbi hendrerit eros sed nisi volutpat, ut sagittis enim ultricies. Praesent iaculis tellus ante, ut feugiat felis molestie sed. Maecenas pharetra purus non tellus iaculis fermentum. Aliquam vel vehicula mi. Integer ac mi luctus, hendrerit enim interdum, commodo dolor. Nunc semper purus orci. ",
                CookTime = 40,
                PrepTime = 30
            });
            var pumpkinChili = await _db.Recipes.AddAsync(new Recipe
            {
                Name = "Pumpkin Chili",
                Instructions = "1. Put the ingredients in a pot.\n2. Cook until done.\n3. Stir occasionally.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur scelerisque sit amet tortor non viverra. In hac habitasse platea dictumst. In vitae magna lobortis enim vestibulum lacinia. Phasellus ornare, orci non fermentum porttitor, nibh neque rutrum erat, in viverra ipsum ipsum posuere risus. Vestibulum pretium id orci vel lacinia. Cras suscipit et nunc in iaculis. Cras maximus in metus quis ullamcorper.\r\n\r\nPellentesque scelerisque malesuada erat, aliquet eleifend augue blandit et. Aliquam tempus felis nec volutpat faucibus. Curabitur eget imperdiet neque, sit amet lacinia orci. Ut viverra, turpis in tempor finibus, nunc leo dapibus lorem, eget tempor tellus tortor quis lectus. Nullam vitae ante interdum, tincidunt leo id, suscipit sapien. Praesent posuere, libero vel accumsan mattis, nisl magna interdum leo, vitae laoreet arcu ipsum sed felis. Morbi varius tempor justo ac mollis. ",
                CookTime = 90,
                PrepTime = 15
            });
            var pumpkinRavioli = await _db.Recipes.AddAsync(new Recipe
            {
                Name = "Pumpkin Ravioli",
                Instructions = "1. Form the ravioli.\n2. Put them in the oven.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla sollicitudin velit a ipsum tincidunt consectetur eu eu nisi. Sed elit elit, eleifend non aliquet a, tincidunt id nisl. Nunc at ligula lacinia, ullamcorper ligula at, congue mauris. Phasellus magna justo, imperdiet vel tincidunt sed, commodo eget nunc. Aenean at pellentesque massa. Praesent sit amet elit pellentesque, molestie tellus ac, euismod ex. Donec consequat purus nec elementum imperdiet. Donec posuere iaculis odio in molestie. In rutrum, leo vel dignissim consequat, risus leo blandit magna, eget mattis dolor augue vitae justo. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam ultricies imperdiet metus, id tincidunt mi convallis sed. ",
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
