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
         * Requires that the id parameter's value is included in the URL segment after pizza/. Remember, the controller-level
         * [Route] attribute defined the /pizza pattern.
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

    // PUT action

    // DELETE action
}