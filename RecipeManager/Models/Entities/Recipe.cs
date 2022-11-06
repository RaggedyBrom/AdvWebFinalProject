using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.Entities
{
    /// <summary>
    /// Models a recipe.
    /// </summary>
    public class Recipe
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PrepTime { get; set; }
        public int CookTime { get; set; }

        // Load in entities from the RecipeIngredient table to get ingredients used in this recipe.
        public ICollection<RecipeIngredient> Ingredients = new List<RecipeIngredient>();
    }
}
