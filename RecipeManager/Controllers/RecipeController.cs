using Microsoft.AspNetCore.Mvc;
using RecipeManager.Models.Entities;
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

        /// <summary>
        /// Reads a single recipe from the repository, converts it to a view model,
        /// and returns a view displaying details of the recipe.
        /// </summary>
        /// <param name="id">The Id of the recipe to find.</param>
        /// <returns>A view displaying details of the recipe if it was found,
        /// or a redirect to the Index method if it was not.</returns>
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

        /// <summary>
        /// Returns a view with an empty form used to create a recipe.
        /// </summary>
        /// <returns>A view with an empty form used to create a recipe.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Adds a new recipe to the repository based on form data from an HTTP POST
        /// request.
        /// </summary>
        /// <param name="recipeVM">A model of a recipe generated from HTTP form data.</param>
        /// <returns>A redirect to the Index method if the creation was successful, or
        /// a redirect to the Create method if the model was invalid.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]RecipeVM recipeVM)
        {
            if (ModelState.IsValid)
            {
                var recipe = recipeVM.GetRecipe();
                var addedRecipe = await _recipeRepo.CreateAsync(recipe);

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Create");
        }

        /// <summary>
        /// Reads a recipe from the repository and sends it to a view where its
        /// values can be edited.
        /// </summary>
        /// <param name="id">The Id of the recipe to find.</param>
        /// <returns>A view containing the recipe model if it was found, or
        /// a redirect to the Index method if it was not found.</returns>
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeRepo.ReadAsync(id);

            if (recipe != null)
            {
                var model = RecipeVM.GetRecipeVM(recipe);
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        /// <summary>
        /// Updates a recipe in the repository based on a model generated from
        /// the form data of an HTTP POST request.
        /// </summary>
        /// <param name="recipeVM">A model of a recipe generated from the form data
        /// of an HTTP POST request.</param>
        /// <returns>A redirect to the Details method for the recipe if the update
        /// was successful, or a redirect to the Index method if the model was
        /// invalid.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]RecipeVM recipeVM)
        {
            if (ModelState.IsValid)
            {
                var recipe = recipeVM.GetRecipe();
                var updatedRecipe = await _recipeRepo.UpdateAsync(recipeVM.Id, recipe);

                if (updatedRecipe != null)
                    return RedirectToAction("Details", updatedRecipe.Id);
                else
                    return RedirectToAction("Edit", recipeVM.Id);
            }
            else
                return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a recipe from the repository.
        /// </summary>
        /// <param name="id">The Id of the recipe to be deleted.</param>
        /// <returns>A redirect to the Index method.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _recipeRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
