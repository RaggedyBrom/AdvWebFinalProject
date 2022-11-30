using RecipeManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.ViewModels
{
    /// <summary>
    /// A view model with data corresponding to a Recipe entity.
    /// </summary>
    public class RecipeVM
    {
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2048)]
        public string? Description { get; set; } = string.Empty;
        [MaxLength(8192)]
        public string Instructions { get; set; } = string.Empty;
        [Required]
        public int PrepTime { get; set; }
        [Required]
        public int CookTime { get; set; }

        public List<RecipeIngredientVM> RecipeIngredients { get; set; } = new List<RecipeIngredientVM>();

        /// <summary>
        /// Creates a RecipeVM with data corresponding to the passed Recipe object.
        /// </summary>
        /// <param name="recipe">A Recipe from which to create a RecipeVM.</param>
        /// <returns>The created RecipeVM object.</returns>
        public static RecipeVM GetRecipeVM(Recipe recipe)
        {
            return new RecipeVM
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                PrepTime = recipe.PrepTime,
                CookTime = recipe.CookTime,

                // For each RecipeIngredient in the Recipe, transform it into a RecipeIngredientVM
                RecipeIngredients = recipe.Ingredients.Select(ri => RecipeIngredientVM.GetRecipeIngredientVM(ri)).ToList()
            };
        }

        /// <summary>
        /// Creates a new Recipe object based on the values of the current view model instance. Ingredients
        /// are not handled and must be assigned elsewhere.
        /// </summary>
        /// <returns>A Recipe object based on the values of the current view model instance.</returns>
        public Recipe GetRecipe()
        {
            return new Recipe
            {
                Name = this.Name,
                Description = this.Description,
                Instructions = this.Instructions,
                PrepTime = this.PrepTime,
                CookTime = this.CookTime
            };
        }
    }
}
