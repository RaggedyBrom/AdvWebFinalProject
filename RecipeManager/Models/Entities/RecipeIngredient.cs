using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.Entities
{
    /// <summary>
    /// Links recipes with ingredients in the database.
    /// </summary>
    public class RecipeIngredient
    {
        public int Id { get; set; }

        public Recipe? Recipe { get; set; }
        public int RecipeId { get; set; }

        public Ingredient? Ingredient { get; set; }
        public int IngredientId { get; set; }

        [MaxLength(64)]
        public string? Amount { get; set; }
        public int? Calories { get; set; }

    }
}
