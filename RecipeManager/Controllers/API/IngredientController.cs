﻿using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// This action method returns a collection of all ingredients found by the repository,
        /// sent as IngredientVMs and formatted as JSON.
        /// </summary>
        /// <returns>An array of IngredientVMs formatted as JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ingredients = await _ingredientRepo.ReadAllAsync();
            var model = ingredients.Select(i => IngredientVM.GetIngredientVM(i));

            return Ok(model);
        }

        /// <summary>
        /// This action method searches for and returns a single Ingredient found by the repository,
        /// sent as an IngredientVM and formatted as JSON.
        /// </summary>
        /// <param name="id">The id of the Ingredient to find.</param>
        /// <returns>A 200 response containing the IngredientVM if found, or a 404 if it was not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ingredient = await _ingredientRepo.ReadAsync(id);
            
            if (ingredient != null)
            {
                var model = IngredientVM.GetIngredientVM(ingredient);
                return Ok(model);
            }
            else
                return NotFound("No resource was found with the given Id.");
        }
    }
}