using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeManager.Models.Entities;
using RecipeManager.Models.ViewModels;
using RecipeManager.Services;
using System;
using System.Net;

namespace RecipeManager.Controllers.API
{
    /// <summary>
    /// Handles API requests that concern recipes. Base route is "api/recipe".
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepo;

        /// <summary>
        /// Default constructor which injects the recipe repository.
        /// </summary>
        /// <param name="recipeRepo">An instance of the recipe repository to be injected.</param>
        public RecipeAPIController(IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        /// <summary>
        /// This action method returns a collection of all recipes found by the repository, sent as RecipeVMs and formatted as JSON.
        /// </summary>
        /// <returns>An array of RecipeVMs formatted as JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recipes = await _recipeRepo.ReadAllAsync();

            var model = recipes.Select(r => RecipeVM.GetRecipeVM(r));

            return Ok(model);
        }

        /// <summary>
        /// This action method searches for and returns a single recipe, sent as RecipeVMs and formatted as JSON.
        /// </summary>
        /// <param name="id">The Id of the recipe to be returned.</param>
        /// <returns>A 200 response with the RecipeVM formatted as JSON if the Recipe was found,
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
                return NotFound("No resource was found with the given Id.");
        }

        /// <summary>
        /// This action method takes in a RecipeVM, generates a Recipe from it, and uses the repository to store it to the database.
        /// </summary>
        /// <param name="recipe">A RecipeVM object created from HTTP form data.</param>
        /// <returns>A 201 response containing a RecipeVM representing the Recipe if the creation was successful,
        /// or a 400 response if it was unsuccessful.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]RecipeVM recipeVM)
        {
            if (ModelState.IsValid)
            {
                var recipe = recipeVM.GetRecipe();
                await _recipeRepo.CreateAsync(recipe);

                var model = RecipeVM.GetRecipeVM(recipe);
                return CreatedAtAction("Get", new { id = recipe.Id }, model);
            }
            else
                return BadRequest("The resource provided was malformed.");
        }

        /// <summary>
        /// This action method uses the repository to delete a specified Recipe.
        /// </summary>
        /// <param name="id">The Id of the Recipe to be deleted.</param>
        /// <returns>A 204 response if the Recipe was deleted, or a 404 response if it was not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _recipeRepo.DeleteAsync(id);

            if (deleted)
                return NoContent();
            else
                return NotFound("No resource was found with the given Id.");
        }

        /// <summary>
        /// This action method uses the reposistory to replace a specified Recipe.
        /// </summary>
        /// <param name="recipeVM">A RecipeVM created from HTTP form data.</param>
        /// <param name="id">The Id of the Recipe to be replaced.</param>
        /// <returns>A 204 response if the Recipe was replaced, or a 404 response if it was not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm]RecipeVM recipeVM)
        {
            if (ModelState.IsValid)
            {
                var recipe = await _recipeRepo.UpdateAsync(id, recipeVM.GetRecipe());

                if (recipe != null)
                {
                    return NoContent();
                }
                else
                    return NotFound("Cannot replace resource with the given Id.");
            }
            else
                return BadRequest("The resource provided was malformed.");
        }

        /// <summary>
        /// This action method uses the repository to get a collection of all the RecipeIngredients
        /// for a given Recipe.
        /// </summary>
        /// <param name="id">The Id of the Recipe to search for.</param>
        /// <returns>A 200 response containing a collection of RecipeIngredientVMs if successful,
        /// or a 404 if the specified Recipe was not found.</returns>
        [HttpGet("{id}/ingredient")]
        public async Task<IActionResult> GetIngredients(int id)
        {
            var recipe = await _recipeRepo.ReadAsync(id);

            if (recipe != null)
            {
                var model = recipe.Ingredients.Select(i => RecipeIngredientVM.GetRecipeIngredientVM(i));
                return Ok(model);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// This action method uses the repository to get a specific RecipeIngredient for
        /// a given Recipe and Ingredient.
        /// </summary>
        /// <param name="id">The Id of the Recipe.</param>
        /// <param name="ingredientId">The Id of the Ingredient.</param>
        /// <returns>A 200 response containing a RecipeIngredientVM corresponding to the RecipeIngredient
        /// if successful, or a 404 if no association exists for the given combination of Ids.</returns>
        [HttpGet("{id}/ingredient/{ingredientId}")]
        public async Task<IActionResult> GetIngredient(int id, int ingredientId)
        {
            var recipeIngredient = await _recipeRepo.ReadIngredientAsync(id, ingredientId);

            if (recipeIngredient != null)
            {
                var model = RecipeIngredientVM.GetRecipeIngredientVM(recipeIngredient);
                return Ok(model);
            }
            else
                return NotFound("No resource was found with the given combination of Ids.");
        }

        /// <summary>
        /// This action method uses the repository to create a new RecipeIngredient for a
        /// given Recipe and Ingredient.
        /// </summary>
        /// <param name="id">The ID of the recipe.</param>
        /// <param name="recipeIngredientVM">A view model for a RecipeIngredient, generated from the request's
        /// form data.</param>
        /// <returns>A 201 response containing a view model for the created RecipeIngredient if the creation was
        /// successful, or a 400 if the creation failed for some reason.</returns>
        [HttpPost("{id}/ingredient")]
        public async Task<IActionResult> PostIngredient(int id, [FromForm]RecipeIngredientVM recipeIngredientVM)
        {
            if (ModelState.IsValid)
            {
                // Attempt to create a connection between the given Recipe and Ingredient Ids
                var ingredientId = recipeIngredientVM.IngredientId;
                var created = await _recipeRepo.CreateIngredientAsync(id, ingredientId);

                // Only continue if the Recipe/Ingredient connection was created
                if (created != null)
                {
                    // Get the model from the form data, and use it to update the RecipeIngredient that was just created
                    var recipeIngredient = recipeIngredientVM.GetRecipeIngredient();
                    recipeIngredient = await _recipeRepo.UpdateIngredientAsync(id, ingredientId, recipeIngredient);

                    var model = RecipeIngredientVM.GetRecipeIngredientVM(recipeIngredient!);
                    return CreatedAtAction("GetIngredient", new { id = id, ingredientId = ingredientId }, model);
                }
                else
                {
                    return BadRequest("The resource cannot be created because the given combination of Ids was invalid.");         
                }
            }
            else
                return BadRequest("The resource provided was malformed.");
        }

        /// <summary>
        /// This action method uses the repository to delete the association between
        /// a given Recipe and Ingredient.
        /// </summary>
        /// <param name="id">The Id of the Recipe.</param>
        /// <param name="ingredientId">The Id of the Ingredient.</param>
        /// <returns>A 204 response if the deletion was successful, or a 404 otherwise.</returns>
        [HttpDelete("{id}/ingredient/{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int id, int ingredientId)
        {
            var deleted = await _recipeRepo.DeleteIngredientAsync(id, ingredientId);

            if (deleted)
                return NoContent();
            else
                return NotFound("No resource was found with the given Id.");
        }

        /// <summary>
        /// This action method uses the repository to update the association between
        /// a given Recipe and Ingredient.
        /// </summary>
        /// <param name="id">The Id of the Recipe.</param>
        /// <param name="ingredientId">The Id of the Ingredient.</param>
        /// <param name="recipeIngredientVM"></param>
        /// <returns>A 204 response if the update was successful, a 404 if the association does not
        /// exist, or a 400 if the association data is malformed.</returns>
        [HttpPut("{id}/ingredient/{ingredientId}")]
        public async Task<IActionResult> PutIngredient(int id, int ingredientId, [FromForm]RecipeIngredientVM recipeIngredientVM)
        {
            if (ModelState.IsValid)
            {
                var recipeIngredient = await _recipeRepo.UpdateIngredientAsync(id, ingredientId, recipeIngredientVM.GetRecipeIngredient());

                if (recipeIngredient != null)
                {
                    return NoContent();
                }
                else
                    return NotFound("Cannot replace resource with the given combination of Ids.");
            }
            else
                return BadRequest("The resource provided was malformed.");
        }
    }
}
