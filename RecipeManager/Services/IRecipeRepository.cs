using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    /// <summary>
    /// Defines methods required for a repository implementation of the Recipe entity.
    /// </summary>
    public interface IRecipeRepository
    {

        /// <summary>
        /// Reads all recipes from the database. Uses eager loading to bring in related data.
        /// </summary>
        /// <returns>A collection of recipes from the database.</returns>
        public Task<ICollection<Recipe>> ReadAllAsync();

        /// <summary>
        /// Reads a single recipe from the database. Uses eager loading to bring in related data.
        /// </summary>
        /// <param name="recipeId">The Id of the desired recipe.</param>
        /// <returns>A reference to desired recipe, or null if it was not found.</returns>
        public Task<Recipe?> ReadAsync(int recipeId);

        /// <summary>
        /// Creates a new recipe in the database.
        /// </summary>
        /// <param name="newRecipe">A recipe to be added to the database.</param>
        /// <returns>A boolean incidicating whether or not the operation was successful.</returns>
        public Task<Recipe> CreateAsync(Recipe newRecipe);

        /// <summary>
        /// Updates an existing recipe in the database.
        /// </summary>
        /// <param name="recipeId">The Id of the recipe to be altered.</param>
        /// <param name="updatedRecipe">A recipe object which will be used to update the existing database entity.</param>
        /// <returns>A reference to the recipe that was updated, or null if the operation was unsuccessful.</returns>
        public Task<Recipe?> UpdateAsync(int recipeId, Recipe updatedRecipe);

        /// <summary>
        /// Deletes a recipe from the database.
        /// </summary>
        /// <param name="recipeId">The Id of the recipe to be deleted.</param>
        /// <returns>A boolean incidicating whether or not the operation was successful.</returns>
        public Task<bool> DeleteAsync(int recipeId);

        /// <summary>
        /// Reads the association between a recipe and an ingredient from the database.
        /// </summary>
        /// <param name="recipeId">The Id of the recipe.</param>
        /// <param name="ingredientId">The Id of the ingredient.</param>
        /// <returns>The associative entity if the association was found,
        /// or null if the association does not exist.</returns>
        public Task<RecipeIngredient?> ReadIngredientAsync(int recipeId, int ingredientId);

        /// <summary>
        /// Creates an association between a recipe and an ingredient in the database.
        /// </summary>
        /// <param name="recipeId">The Id of the recipe.</param>
        /// <param name="ingredientId">The Id of the ingredient.</param>
        /// <returns>The recipe part of the association, or null if the association
        /// was not created.</returns>
        public Task<RecipeIngredient?> CreateIngredientAsync(int recipeId, int ingredientId);

        /// <summary>
        /// Updates the association between a recipe and an ingredient in the database.
        /// </summary>
        /// <param name="recipeId">The Id of the recipe.</param>
        /// <param name="ingredientId">The Id of the ingredient.</param>
        /// <param name="updatedRecipeIngredient">A model representing the relationship between
        /// a recipe and an ingredient.</param>
        /// <returns>The associative entity if the association was updated, or null if the association was not
        /// updated.</returns>
        public Task<RecipeIngredient?> UpdateIngredientAsync(int recipeId, int ingredientId, RecipeIngredient updatedRecipeIngredient);

        /// <summary>
        /// Deletes the association between a recipe and an ingredient in the database.
        /// </summary>
        /// <param name="recipeId">The Id of the recipe.</param>
        /// <param name="ingredientId">The Id of the ingredient.</param>
        /// <returns>A boolean indicating whether or not the operation was successful.</returns>
        public Task<bool> DeleteIngredientAsync(int recipeId, int ingredientId);
    }
}
