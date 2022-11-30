using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.Entities
{
    /// <summary>
    /// Models a recipe.
    /// </summary>
    public class Recipe
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

        // Load in entities from the RecipeIngredient table to get ingredients used in this recipe.
        public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    }
}
