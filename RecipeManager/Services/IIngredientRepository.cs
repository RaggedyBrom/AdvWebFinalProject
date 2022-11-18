using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    /// <summary>
    /// Defines methods required for a repository implementation of the Ingredient entity.
    /// </summary>
    public interface IIngredientRepository
    {
        /// <summary>
        /// Reads all ingredients from the database.
        /// </summary>
        /// <returns>A collection of ingredients from the database.</returns>
        public Task<ICollection<Ingredient>> ReadAllAsync();

        /// <summary>
        /// Reads a single ingredient from the database.
        /// </summary>
        /// <param name="ingredientId">The Id of the desired ingredient.</param>
        /// <returns>A reference to desired ingredient, or null if it was not found.</returns>
        public Task<Ingredient?> ReadAsync(int ingredientId);

        /// <summary>
        /// Creates a new ingredient in the database.
        /// </summary>
        /// <param name="newIngredient">An ingredient to be added to the database.</param>
        /// <returns>A boolean incidicating whether or not the operation was successful.</returns>
        public Task<Ingredient> CreateAsync(Ingredient newIngredient);

        /// <summary>
        /// Updates an existing ingredient in the database.
        /// </summary>
        /// <param name="ingredientId">The Id of the ingredient to be altered.</param>
        /// <param name="updatedIngredient">An ingredient object which will be used to update the existing database entity.</param>
        /// <returns>A reference to the ingredient that was updated, or null if the operation was unsuccessful.</returns>
        public Task<Ingredient?> UpdateAsync(int ingredientId, Ingredient updatedIngredient);

        /// <summary>
        /// Deletes an ingredient from the database.
        /// </summary>
        /// <param name="ingredientId">The Id of the ingredient to be deleted.</param>
        /// <returns>A boolean incidicating whether or not the operation was successful.</returns>
        public Task<bool> DeleteAsync(int ingredientId);
    }
}
