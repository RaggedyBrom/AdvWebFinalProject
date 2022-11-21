using RecipeManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.ViewModels
{
    /// <summary>
    /// A view model with data corresponding to an Ingredient entity.
    /// </summary>
    public class IngredientVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IngredientType Type { get; set; }

        /// <summary>
        /// Creates a new IngredientVM object based on the values of an Ingredient instance.
        /// </summary>
        /// <param name="ingredient">The Ingredient object from which the IngredientVM will be created.</param>
        /// <returns>An IngredientVM object based on the values of the input Ingredient instance.</returns>
        public static IngredientVM GetIngredientVM(Ingredient ingredient)
        {
            return new IngredientVM
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Type = ingredient.Type
            };
        }

        /// <summary>
        /// Creates a new Ingredient object based on the values of the current view model instance.
        /// </summary>
        /// <returns>A Ingredient object based on the values of the current view model instance.</returns>
        public Ingredient GetIngredient()
        {
            return new Ingredient
            {
                Name = this.Name,
                Type = this.Type
            };
        }
    }
}
