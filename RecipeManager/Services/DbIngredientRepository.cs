using Microsoft.EntityFrameworkCore;
using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    /// <summary>
    /// Provides implementation for database operations with Ingredient entities.
    /// </summary>
    public class DbIngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Default constructor which accepts an injected database context.
        /// </summary>
        /// <param name="db">The database context that will be used by the repository
        /// to carry out database operations</param>
        public DbIngredientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Defined in IIngredientRepository
        public async Task<Ingredient> CreateAsync(Ingredient newIngredient)
        {
            await _db.Ingredients.AddAsync(newIngredient);
            await _db.SaveChangesAsync();
            return newIngredient;
        }

        // Defined in IIngredientRepository
        public async Task<bool> DeleteAsync(int ingredientId)
        {
            var ingredient = await ReadAsync(ingredientId);

            if (ingredient == null)
                return false;
            else
            {
                _db.Ingredients.Remove(ingredient);
                await _db.SaveChangesAsync();
                return true;
            }
        }

        // Defined in IIngredientRepository
        public async Task<ICollection<Ingredient>> ReadAllAsync()
        {
            return await _db.Ingredients.ToListAsync();
        }

        // Defined in IIngredientRepository
        public async Task<Ingredient?> ReadAsync(int ingredientId)
        {
            return await _db.Ingredients
                .Include(i => i.Recipes)
                    .ThenInclude(ri => ri.Recipe)
                .FirstOrDefaultAsync(i => i.Id == ingredientId);
        }

        // Defined in IIngredientRepository
        public async Task<Ingredient?> UpdateAsync(int ingredientId, Ingredient updatedIngredient)
        {
            var ingredient = await ReadAsync(ingredientId);

            if (ingredient != null)
            {
                ingredient.Name = updatedIngredient.Name;
                ingredient.Type = updatedIngredient.Type;
            }

            return ingredient;
        }
    }
}
