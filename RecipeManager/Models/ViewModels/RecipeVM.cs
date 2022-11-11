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
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2048)]
        public string? Description { get; set; } = string.Empty;
        [MaxLength(8192)]
        public string Instructions { get; set; } = string.Empty;
        public int PrepTime { get; set; }
        public int CookTime { get; set; }

        List<RecipeIngredientVM> RecipeIngredients { get; set; } = new List<RecipeIngredientVM>();

        /// <summary>
        /// Creates a new Recipe object based on the values of the current view model instance. Ingredients
        /// are not handled and must be assigned elsewhere.
        /// </summary>
        /// <returns>A Recipe object based on the values of the current view model instance.</returns>
        public Recipe GetRecipe()
        {
            return new Recipe
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Instructions = this.Instructions,
                PrepTime = this.PrepTime,
                CookTime = this.CookTime
            };
        }
    }
}
