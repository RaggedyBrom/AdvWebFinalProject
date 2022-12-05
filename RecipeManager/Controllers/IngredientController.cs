using Microsoft.AspNetCore.Mvc;
using RecipeManager.Models.ViewModels;
using RecipeManager.Services;

namespace RecipeManager.Controllers
{
    /// <summary>
    /// Controller which returns views relevant to ingredients.
    /// </summary>
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepo;

        /// <summary>
        /// Default constructor which injects an instance of the ingredient
        /// repository.
        /// </summary>
        /// <param name="ingredientRepo">An instance of the ingredient repository
        /// used to query the database.</param>
        public IngredientController(IIngredientRepository ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
        }

        /// <summary>
        /// Reads all ingredients from the repository, converts them to view models,
        /// and returns a view displaying a list of the ingredients.
        /// </summary>
        /// <returns>A view that displays a list of IngredientVMs.</returns>
        public async Task<IActionResult> Index()
        {
            var ingredients = await _ingredientRepo.ReadAllAsync();
            var model = ingredients.Select(i => IngredientVM.GetIngredientVM(i));
            return View(model);
        }

        /// <summary>
        /// Reads all the ingredients from the repository, converts them to view models,
        /// and returns a partial view displaying a list of the ingredients along with "add"
        /// buttons.
        /// </summary>
        /// <returns>A partial view that displays a list of IngredientVMs along with
        /// "add" buttons.</returns>
        public async Task<IActionResult> AddIngredientPartial()
        {
            var ingredients = await _ingredientRepo.ReadAllAsync();
            var model = ingredients.Select(i => IngredientVM.GetIngredientVM(i));
            return PartialView("_AddIngredient", model);
        }
    }
}
