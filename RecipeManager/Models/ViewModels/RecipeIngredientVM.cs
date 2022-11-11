using RecipeManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.ViewModels
{
    /// <summary>
    /// A view model with data corresponding to a RecipeIngredient entity.
    /// </summary>
    public class RecipeIngredientVM
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }
        [MaxLength(64)]
        public string IngredientName { get; set; } = string.Empty;

        public FoodGroup FoodGroup { get; set; }

        public int Quantity { get; set; }
        [MaxLength(64)]
        public string? QuantityUnit { get; set; }
        public int? Calories { get; set; }
    }
}
