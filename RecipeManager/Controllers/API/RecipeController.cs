using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeManager.Models.Entities;
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
        /// This action method returns a collection of all recipes found by the repo, formatted as JSON.
        /// </summary>
        /// <returns>A task indicating the result of the action method.</returns>
        [HttpGet]
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recipe = await _recipeRepo.ReadAsync(id);
            return Ok(recipe);
        }

        /// <summary>
        /// This action method takes in a recipe and uses the repo to store it to the database.
        /// </summary>
        /// <param name="recipe">A recipe object created from HTTP form data.</param>
        /// <returns>A task indicating the result of the action method.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                await _recipeRepo.CreateAsync(recipe);
                return Ok();
            }
            return BadRequest();
                
        }
    }
}
