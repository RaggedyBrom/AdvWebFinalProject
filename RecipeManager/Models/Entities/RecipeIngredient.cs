using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.Entities
{
    /// <summary>
    /// Links recipes with ingredients in the database.
    /// </summary>
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        [MaxLength(64)]
        public string? QuantityUnit { get; set; }
        public int? Calories { get; set; }

    }
}
