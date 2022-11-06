using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    /// <summary>
    /// Defines methods required for a repository implementation of the Recipe entity.
    /// </summary>
    public interface IRecipeRepository
    {
        public Task<ICollection<Recipe>> ReadAllAsync();

        public Task<Recipe?> ReadAsync(int recipeId);

        public Task<bool> CreateAsync(Recipe recipe);

        public Task<bool> UpdateAsync(int recipeId, Recipe recipe);

        public Task<bool> DeleteAsync(int recipeId);
    }
}
