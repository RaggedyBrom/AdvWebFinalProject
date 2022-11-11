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
    }
}
