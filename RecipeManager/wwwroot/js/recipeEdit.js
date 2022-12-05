"use strict";

import { createIngredient } from "./ingredientRepository.js";
import { addIngredient, removeIngredient, updateIngredient } from "./recipeRepository.js";

const addIngredientModal = $("#addIngredientModal");
const editIngredientModal = $("#editIngredientModal");
const createIngredientModal = $("#createIngredientModal");
const ingredientsTable = $("#ingredientsTable");
const recipeId = $("#recipeIdInput").val();

// Event listener to handle clicks on the modal buttons that adds an ingredient
// to the recipe
addIngredientModal.on("click", ".addIngredientBtn", async (e) => {

    // Grab the ingredient Id that was stored in the button's value attribute
    const ingredientId = $(e.target).val();

    // Use the repository to create the relationship between the recipe and ingredient, and get back
    // the JSON data representing the connection
    await addIngredient(recipeId, ingredientId);

    // Construct the ingredient row data from the modal row
    const row = $(e.target).parent().parent();
    const rowData = {
        ingredientName: row.find(".ingredientName").text(),
        ingredientType: row.find(".ingredientType").text(),
        ingredientId: ingredientId
    };

    // Add the new ingredient to the table and hide the modal
    createIngredientRow(rowData);
    addIngredientModal.modal("hide");
});

// Event listener to handle clicks on the button that activates the Create Ingredient modal
addIngredientModal.on("click", ".createIngredientModalBtn", async (e) => {

    // Hide the Add Ingredient modal and show the Create Ingredient one
    addIngredientModal.modal("hide");
    createIngredientModal.modal("show");

});

// Event listener to handle events on the button the submits the Create Ingredient form
createIngredientModal.on("click", ".createIngredientBtn", async (e) => {

    // Prevent the form from submitting regularly
    e.preventDefault();

    // Grab the data from the form
    const formData = new FormData($("#createIngredientForm")[0]);

    // Use the repository to create the ingredient and store the returned JSON
    const ingredient = await createIngredient(formData);

    // Use the repository to add the newly-created ingredient to the recipe
    const recipeIngredient = await addIngredient(recipeId, ingredient.id);

    // Construct the row data from the form and created ingredient
    const rowData = {
        ingredientName: $("#createIngredientName").val(),
        ingredientType: $("#createIngredientType").val(),
        ingredientId: ingredient.id
    };

    // Add a new table row for the new ingredient and hide the Create Ingredient modal
    createIngredientRow(rowData);
    createIngredientModal.modal("hide");
});


// Event listener to handle clicks on the button that displays the Add Ingredient modal
addIngredientModalBtn.addEventListener("click", async (e) => {

    const addIngredientModalBody = $("#addIngredientModalBody")[0];

    // Retrieve the partial view that will go inside the modal
    const modalContent = await getAddIngredientPartial();

    // Add the retrieved content to the modal's body and set the modal to be displayed
    addIngredientModalBody.innerHTML = "";
    addIngredientModalBody.insertAdjacentHTML("afterbegin", modalContent);
    addIngredientModal.modal("show");
});


// Event listener to handle clicks on the table buttons that deletes an ingredient
// from the recipe
ingredientsTable.on("click", ".deleteIngredientBtn", async (e) => {

    const row = $(e.target).parent().parent();      // Grab the row that the button is in
    const ingredientId = $(e.target).val();         // Grab the ingredient's Id stored in the button's value

    // Use the repository to remove the ingredient from the recipe
    const response = await removeIngredient(recipeId, ingredientId);

    // Only remove the row if the operation was successful
    if (response == 204) {
        row.remove();
    }
});


// Event listener to handle clicks on the table buttons that edit an ingredient
// of the recipe
ingredientsTable.on("click", ".editIngredientBtn", async (e) => {

    const row = $(e.target).parent().parent();      // Grab the row that the button is in
    const ingredientId = $(e.target).val();         // Grab the ingredient's Id stored in the button's value

    // Grab all the ingredient data from the row cells
    const ingredientName = row.find(".ingredientNameCell").text().trim();
    const ingredientType = row.find(".ingredientTypeCell").text().trim();
    const ingredientAmount = row.find(".ingredientAmountCell").text().trim();
    const ingredientCalories = row.find(".ingredientCaloriesCell").text().trim();
    
    // Set the row's Id to "returnRow" so that we can find it after completing the edit
    row.attr("id", "returnRow");

    // Add the ingredient data to the modal input fields and display the modal
    $("#editIngredientNameInput").val(ingredientName);
    $("#editIngredientTypeInput").val(ingredientType);
    $("#editIngredientAmountInput").val(ingredientAmount);
    $("#editIngredientCaloriesInput").val(ingredientCalories);
    $("#editIngredientIdInput").val(ingredientId);
    editIngredientModal.modal("show");
});


// Event listener to handle clicks on the Save button of the Edit Ingredient modal
editIngredientModal.on("click", "#editIngredientSaveBtn", async (e) => {

    // Prevent the form from submitting regularly
    e.preventDefault();

    const formData = new FormData($("#editIngredientForm")[0]);     // Get the form data from the edit ingredient form
    const ingredientId = $("#editIngredientIdInput").val()          // Get the ingredient Id that was stored in the hidden input

    // Use the repository to update the recipe ingredient using the form data
    await updateIngredient(recipeId, ingredientId, formData);

    const row = $("#returnRow");                                    // Get the table row that the ingredient data was originally loaded from

    // Grab data from the modal and use it to update the cells of the table row
    const updatedAmount = $("#editIngredientAmountInput").val();
    const updatedCalories = $("#editIngredientCaloriesInput").val();
    row.find(".ingredientAmountCell").text(updatedAmount);
    row.find(".ingredientCaloriesCell").text(updatedCalories);

    // Remove the "returnRow" Id from the row and hide the modal
    row.attr("id", "");
    editIngredientModal.modal("hide");
});


/*
 *  Sends an AJAX request to retrieve the Add Ingredient partial view from the server.
 *  Returns the HTML for the partial view.
 */
async function getAddIngredientPartial() {
    const address = window.location.origin + "/Ingredient/AddIngredientPartial"

    const response = await fetch(address);

    if (!response.ok) {
        throw new Error("There was an HTTP error retrieving the add ingedient partial view.")
    }
    return await response.text();
}

/*
 *  Creates a new row in the ingredient table and fills it with ingredient data.
 *  ingredient: An object containing values corresponding to ingredient data.
 */
function createIngredientRow(ingredient) {

    // Create a copy of the blank ingredient row (blank row exists even if the table has no ingredients)
    const row = $("#blankIngredientRow").clone();

    // Remove the id and styling from the row so that it won't be hidden
    row.attr("id", "").attr("style", "");

    // Add data to the table taken from the parameter object's values
    row.find(".ingredientNameCell").append(ingredient.ingredientName);
    row.find(".ingredientTypeCell").append(ingredient.ingredientType);
    row.find(".editIngredientBtn").val(ingredient.ingredientId);
    row.find(".deleteIngredientBtn").val(ingredient.ingredientId);

    // Add the row to the end of the ingredients table
    ingredientsTable.append(row);
}