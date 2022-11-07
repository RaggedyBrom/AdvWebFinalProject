using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeManager.Services;

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
        /// This action method returns a collection of all recipes found by the repo, formatted as JSON.
        /// </summary>
        /// <returns>A task indicating the result of the action method.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var recipes = await _recipeRepo.ReadAllAsync();
            return Ok(recipes);
        }

        /// <summary>
        /// This action method searches for and returns a single recipe, formatted as JSON.
        /// </summary>
        /// <param name="id">The Id of the recipe to be returned.</param>
        /// <returns>A task indicating the result of the action method.</returns>
        [HttpGet("one/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recipe = await _recipeRepo.ReadAsync(id);
            return Ok(recipe);
        }
    }
}
