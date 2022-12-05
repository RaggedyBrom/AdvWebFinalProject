"use strict";

const baseAddress = window.location.origin + "/api/recipe";     // Base address for the recipe API endpoint

/*  
 *  Sends an AJAX request to add an ingredient to a recipe.
 *  recipeId: The int Id of the recipe.
 *  ingredientId: The int Id of the ingredient.
 *  Returns the JSON data of the response.
 */
export async function addIngredient(recipeId, ingredientId) {
    const address = baseAddress + "/" + recipeId + "/ingredient";

    // Create empty form data and add the ingredient Id to it, since the view model requires an Id to be valid
    const formData = new FormData();
    formData.append("IngredientId", ingredientId);

    // Send the request to the endpoint address, using a POST method and including the created form data
    const response = await fetch(address, {
        method: "post",
        body: formData
    });

    if (!response.ok) {
        throw new Error("There was an HTTP error retrieving the ingredient data.");
    }
    return await response.json();
}

/*
 *  Sends an AJAX request to remove an ingredient from a recipe.
 *  recipeId: The int Id of the recipe.
 *  ingredientId: The int Id of the ingredient.
 *  Returns the HTTP status code of the response.
 */
export async function removeIngredient(recipeId, ingredientId) {
    const address = baseAddress + "/" + recipeId + "/ingredient/" + ingredientId;

    // Send the request to the endpoint address, using a DELETE method
    const response = await fetch(address, {
        method: "delete"
    });

    if (!response.ok) {
        throw new Error("There was an HTTP error removing the ingredient.");
    }
    return await response.status;
}

/*  
 *  Sends an AJAX request to update a recipe ingredient.
 *  RecipeId: The int Id of the recipe.
 *  IngredientId: The int Id of the ingredient.
 *  formDdata: HTTP form data containing the information that will be used
 *      to update the recipe ingredient.
 *  Returns the HTTP status code of the response.
 */
export async function updateIngredient(recipeId, ingredientId, formData) {
    const address = baseAddress + "/" + recipeId + "/ingredient/" + ingredientId;

    // Send the request to the endpoint address, using a PUT method and including the
    // supplied form data
    const response = await fetch(address, {
        method: "put",
        body: formData
    });

    if (!response.ok) {
        throw new Error("There was an HTTP error updating the ingredient.");
    }
    return await response.status;
}