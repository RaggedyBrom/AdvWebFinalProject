using Microsoft.EntityFrameworkCore;
using RecipeManager.Models.Entities;

namespace RecipeManager.Services
{
    /// <summary>
    /// Serves as a context between the application and the database, with DbSet
    /// properties corresponding to entities in the database and models in the
    /// application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Default constructor which takes in options and passes them to the base class.
        /// </summary>
        /// <param name="options">Options to apply to this application's implementation of the DbContext.</param>
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Recipe> Recipe => Set<Recipe>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    }
}
