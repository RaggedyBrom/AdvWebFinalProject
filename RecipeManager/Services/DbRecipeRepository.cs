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
        public async Task<RecipeIngredient?> CreateIngredientAsync(int recipeId, int ingredientId)
        {
            var recipe = await ReadAsync(recipeId);
            var ingredient = await _db.Ingredients.FirstOrDefaultAsync(i => i.Id == ingredientId);

            RecipeIngredient? recipeIngredient;

            if (recipe != null && ingredient != null)
            {
                recipeIngredient = new RecipeIngredient();
                recipeIngredient.Recipe = recipe;
                recipeIngredient.Ingredient = ingredient;

                recipe.Ingredients.Add(recipeIngredient);
                ingredient.Recipes.Add(recipeIngredient);

                await _db.SaveChangesAsync();
            }
            else
                recipeIngredient = null;

            return recipeIngredient;
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
        public async Task<bool> DeleteIngredientAsync(int recipeId, int ingredientId)
        {
            var recipeIngredient = await ReadIngredientAsync(recipeId, ingredientId);

            if (recipeIngredient == null)
                return false;
            else
            {
                _db.RecipeIngredients.Remove(recipeIngredient);
                await _db.SaveChangesAsync();
                return true;
            }
        }

        // Defined in IRecipeRepository
        public async Task<ICollection<Recipe>> ReadAllAsync()
        {
            return await _db.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .ToListAsync();
        }

        // Defined in IRecipeRepository
        public async Task<Recipe?> ReadAsync(int recipeId)
        {
            return await _db.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
        }

        // Defined in IRecipeRepository
        public async Task<RecipeIngredient?> ReadIngredientAsync(int recipeId, int ingredientId)
        {
            return await _db.RecipeIngredients.FirstOrDefaultAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == ingredientId);
        }

        // Defined in IRecipeRepository
        public async Task<Recipe?> UpdateAsync(int recipeId, Recipe updatedRecipe)
        {
            var dbRecipe = await ReadAsync(recipeId);

            if (dbRecipe != null)
            {
                dbRecipe.Name = updatedRecipe.Name;
                dbRecipe.Instructions = updatedRecipe.Instructions;
                dbRecipe.Description = updatedRecipe.Description;
                dbRecipe.PrepTime = updatedRecipe.PrepTime;
                dbRecipe.CookTime = updatedRecipe.CookTime;
                dbRecipe.Ingredients = updatedRecipe.Ingredients;

                await _db.SaveChangesAsync();
            }

            return dbRecipe;
        }

        // Defined in IRecipeRepository
        public async Task<RecipeIngredient?> UpdateIngredientAsync(int recipeId, int ingredientId, RecipeIngredient updatedRecipeIngredient)
        {
            var recipeIngredient = await ReadIngredientAsync(recipeId, ingredientId);

            if (recipeIngredient != null)
            {
                recipeIngredient.Quantity = updatedRecipeIngredient.Quantity;
                recipeIngredient.QuantityUnit = updatedRecipeIngredient.QuantityUnit;
                recipeIngredient.Calories = updatedRecipeIngredient.Calories;

                await _db.SaveChangesAsync();
            }
            return recipeIngredient;
        }
    }
}
