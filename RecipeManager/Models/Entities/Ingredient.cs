using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.Entities
{
    /// <summary>
    /// Models an ingredient.
    /// </summary>
    public class Ingredient
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
        public FoodGroup? FoodGroup { get; set; }
    }

    public enum FoodGroup
    {
        Fruit,
        Vegetable,
        Grain,
        Protein,
        Dairy
    }
}
