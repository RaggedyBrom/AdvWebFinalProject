"use strict";

const baseAddress = window.location.origin + "/api/ingredient";     // Base address for the ingredient API endpoint

/*  
 *  Sends an AJAX request to create a new ingredient.
 *  formData: Form data containing information that will be used to
 *  create the new ingredient
 *  Returns the JSON data of the response.
 */
export async function createIngredient(formData) {
    const address = baseAddress;

    // Send the request to the endpoint address, using a POST method and including the form data
    const response = await fetch(address, {
        method: "post",
        body: formData
    });

    if (!response.ok) {
        throw new Error("There was an HTTP error retrieving the ingredient data.");
    }
    return await response.json();
}