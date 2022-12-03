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

        /// <summary>
        /// Default constructor which takes in a database context.
        /// </summary>
        /// <param name="db">The database context that the class will use
        /// to carry out the seeding operation.</param>
        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
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

            await _db.Recipes.AddAsync(new Recipe
            {
                Name = "",
                Instructions = "",
                Description = "",
                CookTime = 0,
                PrepTime = 0
            });

            await _db.Ingredients.AddAsync(new Ingredient
            {
                Name = "",
                Type = IngredientType.Fruit
            });

            await _db.SaveChangesAsync();
        }
    }
}
