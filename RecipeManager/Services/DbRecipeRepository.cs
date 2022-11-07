using Microsoft.EntityFrameworkCore;
using RecipeManager.Models.Entities;
using System.Reflection;

namespace RecipeManager.Services
{
    /// <summary>
    /// Provides implementation for database operations with Recipe entities.
    /// </summary>
    public class DbRecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Default constructor which accepts an injected database context.
        /// </summary>
        /// <param name="db">The database context that will be used by the repository
        /// to carry out database operations.</param>
        public DbRecipeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Defined in IRecipeRepository
        public async Task<Recipe> CreateAsync(Recipe newRecipe)
        {
            await _db.Recipes.AddAsync(newRecipe);
            await _db.SaveChangesAsync();
            return newRecipe;
        }

        // Defined in IRecipeRepository
        public async Task<bool> DeleteAsync(int recipeId)
        {
            var recipe = await ReadAsync(recipeId);

            if (recipe == null)
                return false;
            else
            {
                _db.Recipes.Remove(recipe);
                await _db.SaveChangesAsync();
                return true;
            }
        }

        // Defined in IRecipeRepository
        public async Task<ICollection<Recipe>> ReadAllAsync()
        {
            return await _db.Recipes.ToListAsync();
        }

        // Defined in IRecipeRepository
        public async Task<Recipe?> ReadAsync(int recipeId)
        {
            return await _db.Recipes.FindAsync(recipeId);
        }

        // Defined in IRecipeRepository
        public async Task<Recipe?> UpdateAsync(int recipeId, Recipe updatedRecipe)
        {
            var dbRecipe = await _db.Recipes.FindAsync(recipeId);

            if (dbRecipe != null)
            {
                dbRecipe.Name = updatedRecipe.Name;
                dbRecipe.Description = updatedRecipe.Description;
                dbRecipe.PrepTime = updatedRecipe.PrepTime;
                dbRecipe.CookTime = updatedRecipe.CookTime;
                dbRecipe.Ingredients = updatedRecipe.Ingredients;

                await _db.SaveChangesAsync();
            }

            return dbRecipe;
        }
    }
}
