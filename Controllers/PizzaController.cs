// Tutorial: https://docs.microsoft.com/en-us/learn/modules/build-web-api-aspnet-core/6-exercise-add-controller

/*
 * A controller is a public class with one or more public methods known as actions. By convention, 
 * a controller is placed in the project root's Controllers directory. The actions are exposed as 
 * HTTP endpoints inside the web API controller.
 */

using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

/*
 * As you learned previously, this class derives from ControllerBase, the base class for working with
 * HTTP requests in ASP.NET Core. It also includes the two standard attributes you've learned about,
 * [ApiController] and [Route]. As before, the [Route] attribute defines a mapping to the [controller]
 * token. Because this controller class is named PizzaController, this controller handles requests to https://localhost:{PORT}/pizza.
 */
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    // httprepl https://localhost:7128
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById(int id)
    {
        /*
         * Responds only to the HTTP GET verb, as denoted by the [HttpGet] attribute.
         * >>> Requires that the id parameter's value is included in the URL segment after pizza/. (for example /pizza/3) <<<
         * Remember, the controller-level [Route] attribute defined the /pizza pattern.
         * Queries the database for a pizza that matches the provided id parameter.
         */
        var pizza =  PizzaService.Get(id);
        if (pizza == null)
            return NotFound();
        return pizza;

        //Each ActionResult instance used in the preceding action is mapped to the corresponding HTTP status code in the following table:
        /*
            ASP.NET Core
            action result 	    HTTP status code 	Description

            _Ok_ is implied     200                 A product that matches the provided id parameter exists in the in-memory cache.The product is included in the response body in the media type, as defined in the accept HTTP request header (JSON by default).
            _NotFound_          404                 A product that matches the provided id parameter doesn't exist in the in-memory cache.
         */
    }

    // POST action
    [HttpPost]
    // Returns HTTP status code 201 (CreatedAtAction) if it succeeded. Assumes 400 (BadRequest) otherwise.
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        // The first parameter in the CreatedAtAction method call represents an action name. The nameof keyword
        // is used to avoid hard-coding the action name. CreatedAtAction uses the action name to generate a 
        // location HTTP response header with a URL to the newly created pizza, as explained in the previous unit.
        return CreatedAtAction(
            nameof(Create), // actionName: The name of the action to use for generating the URL.
            new {id = pizza.Id}, // routeValues: The route data to use for generating the URL. (this should generate /Create/Id )
            pizza // value: The content value to format in the entity body.
        );
    }

    // PUT action
    [HttpPut("{id}")]
    // Returns HTTP status code 204 (NoContent) if it succeeded. Assumes 400 (BadRequest) otherwise.
    public IActionResult Update(int id, Pizza pizza)
    {
        // A PUT action takes an Id >> and a new object <<. The Id will be used to search for an existing object.
        // If found, this object associated with the Id will be replaced the received object.
        // So a PUT request needs to receive a new object and fully replace the existing one, unlike a PATCH request.

        if (id != pizza.Id)
            return BadRequest();
        
        var existingPizza = PizzaService.Get(id);
        if (existingPizza == null)
            return NotFound();
        
        PizzaService.Update(pizza);
        return NoContent();

        // Returns IActionResult because the ActionResult return type isn't known until runtime. 
        // The BadRequest, NotFound, and NoContent methods return BadRequestResult, NotFoundResult,
        // and NoContentResult types, respectively.
    }

    // DELETE action
    [HttpDelete("{id}")]
    // Returns HTTP status code 204 (NoContent) if it succeeded. Assumes 400 (BadRequest) otherwise.
    public IActionResult Delete(int id)
    {
        var existingPizza = PizzaService.Get(id);
        if (existingPizza == null)
            return NotFound();

        PizzaService.Delete(id);
        return NoContent();
    }
}