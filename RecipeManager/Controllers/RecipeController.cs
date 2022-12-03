using Microsoft.AspNetCore.Mvc;
using RecipeManager.Models.ViewModels;
using RecipeManager.Services;

namespace RecipeManager.Controllers
{
    /// <summary>
    /// Controller which returns views relevant to recipes.
    /// </summary>
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepo;

        /// <summary>
        /// Default contstructor which injects an instance of the
        /// recipe repository.
        /// </summary>
        /// <param name="recipeRepo">An instance of the recipe repository
        /// to be used in database queries.</param>
        public RecipeController(IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        /// <summary>
        /// Reads all the recipes from the repository, converts them to
        /// view models, and returns a view displaying a list of the
        /// recipes.
        /// </summary>
        /// <returns>A view that displays a list of RecipeVMs.</returns>
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeRepo.ReadAllAsync();
            var model = recipes.Select(r => RecipeVM.GetRecipeVM(r));
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeRepo.ReadAsync(id);

            if (recipe != null)
            {
                var model = RecipeVM.GetRecipeVM(recipe);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
