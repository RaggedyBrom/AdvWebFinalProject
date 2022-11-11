using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeManager.Models.Entities;
using RecipeManager.Models.ViewModels;
using RecipeManager.Services;
using System.Net;

namespace RecipeManager.Controllers.API
{
    /// <summary>
    /// Handles API requests that concern recipes. Base route is "api/recipe".
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepo;

        /// <summary>
        /// Default constructor which injects the recipe repository.
        /// </summary>
        /// <param name="recipeRepo">An instance of the recipe repository to be injected.</param>
        public RecipeController(IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        /// <summary>
        /// This action method returns a collection of all recipes found by the repository, formatted as JSON.
        /// </summary>
        /// <returns>An array of recipes formatted as JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recipes = await _recipeRepo.ReadAllAsync();

            var model = recipes.Select(r => RecipeVM.GetRecipeVM(r));

            return Ok(model);
        }

        /// <summary>
        /// This action method searches for and returns a single recipe, formatted as JSON.
        /// </summary>
        /// <param name="id">The Id of the recipe to be returned.</param>
        /// <returns>A 200 response with the recipe formatted as JSON if it was found,
        /// or a 404 if it was not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recipe = await _recipeRepo.ReadAsync(id);

            if (recipe != null)
            {
                var model = RecipeVM.GetRecipeVM(recipe);
                return Ok(model);
            }   
            else
                return NotFound();
        }

        /// <summary>
        /// This action method takes in a recipe and uses the repository to store it to the database.
        /// </summary>
        /// <param name="recipe">A recipe object created from HTTP form data.</param>
        /// <returns>A 201 response containing the recipe and its location if the creation was successful,
        /// or a 400 response if it was not successful.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]RecipeVM recipeVM)
        {
            if (ModelState.IsValid)
            {
                var recipe = recipeVM.GetRecipe();
                await _recipeRepo.CreateAsync(recipe);
                return CreatedAtAction("Get", new { id = recipe.Id }, recipe);
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// This action method uses the repository to delete a specified recipe.
        /// </summary>
        /// <param name="id">The Id of the recipe to be deleted.</param>
        /// <returns>A 204 response if the recipe was deleted, or a 404 response if it was not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _recipeRepo.DeleteAsync(id);

            if (deleted)
                return NoContent();
            else
                return NotFound();
        }
    }
}
