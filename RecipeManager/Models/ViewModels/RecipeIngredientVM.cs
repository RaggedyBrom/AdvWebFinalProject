using RecipeManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.ViewModels
{
    /// <summary>
    /// A view model with data corresponding to a RecipeIngredient entity.
    /// </summary>
    public class RecipeIngredientVM
    {
        public int? Id { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int IngredientId { get; set; }
        [MaxLength(64)]
        public string? IngredientName { get; set; } = string.Empty;

        public IngredientType? IngredientType { get; set; }

        [MaxLength(64)]
        public string? Amount { get; set; }
        public int? Calories { get; set; }

        /// <summary>
        /// Creates a new RecipeIngredient object based on the values of the current view model instance.
        /// </summary>
        /// <returns>A Recipe object based on the values of the current view model instance.</returns>
        public RecipeIngredient GetRecipeIngredient()
        {
            return new RecipeIngredient()
            {
                Amount = this.Amount,
                Calories = this.Calories,
            };
        }

        /// <summary>
        /// Creates a RecipeIngredientVM with data corresponding to the passed RecipeIngredient object.
        /// </summary>
        /// <param name="recipeIngredient">The RecipeIngredient object from which the RecipeIngredientVM will be created.</param>
        /// <returns>The created RecipeIngredientVM object.</returns>
        public static RecipeIngredientVM GetRecipeIngredientVM(RecipeIngredient recipeIngredient)
        {
            return new RecipeIngredientVM
            {
                Id = recipeIngredient.Id,
                IngredientId = recipeIngredient.IngredientId,
                IngredientName = recipeIngredient.Ingredient!.Name,
                IngredientType = recipeIngredient.Ingredient!.Type,
                Amount = recipeIngredient.Amount,
                Calories = recipeIngredient.Calories
            };
        }
    }
}
