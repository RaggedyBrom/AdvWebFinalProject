using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RecipeManager.Models.Entities
{
    /// <summary>
    /// Models an ingredient.
    /// </summary>
    [Index(nameof(Name), IsUnique = true)]
    public class Ingredient
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;
        public IngredientType? Type { get; set; }

        // Load in entities from the RecipeIngredient table to get recipes that use this ingredient.
        public ICollection<RecipeIngredient> Recipes { get; set; } = new List<RecipeIngredient>();
    }

    /// <summary>
    /// Enum with values corresponding to different types of ingredients.
    /// </summary>
    public enum IngredientType
    {
        Fruit,
        Vegetable,
        Grain,
        Bean,
        Meat,
        Nut,
        Seafood,
        Egg,
        Dairy,
        Seed,
        Herb,
        Spice,
        Seasoning,
        Condiment,
        Fat
    }
}
