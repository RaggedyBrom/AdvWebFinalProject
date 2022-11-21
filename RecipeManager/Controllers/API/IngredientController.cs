using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.Models.Entities;
using RecipeManager.Models.ViewModels;
using RecipeManager.Services;

namespace RecipeManager.Controllers.API
{
    /// <summary>
    /// Handles API requests that concern ingredients. Base route is "api/ingredient".
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepo;

        /// <summary>
        /// Default constructor which injects the ingredient repository.
        /// </summary>
        /// <param name="ingredientRepo">An instance of the ingredient repository to be injected.</param>
        public IngredientController(IIngredientRepository ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
        }

        /// <summary>
        /// This action method takes in an IngredientVM from form data, generates an ingredient from it, and
        /// uses the repository to the store it to the database.
        /// </summary>
        /// <param name="ingredientVM">An IngredientVM created from HTTP form data.</param>
        /// <returns>A 201 response containing an IngredientVM representing the Ingredient that was created if
        /// the creation was successful, or a 400 response if it was unsuccessful.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]IngredientVM ingredientVM)
        {
            if (ModelState.IsValid)
            {
                var ingredient = ingredientVM.GetIngredient();
                await _ingredientRepo.CreateAsync(ingredient);

                var model = IngredientVM.GetIngredientVM(ingredient);
                return CreatedAtAction("Get", new { id = ingredient.Id }, model);
            }
            else
                return BadRequest();
        }


    }
}
