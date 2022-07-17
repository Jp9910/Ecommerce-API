
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();
    private static int id = 1;

    [HttpPost]
    public IActionResult Create([FromBody] Movie movie)
    {
        movie.Id = id++; // "var++" gets the value first, than increments by 1
        movies.Add(movie);
        return CreatedAtAction(
            nameof(GetById), // Action that can be used to retrieve the just created resource
            new {Id = movie.Id}, // The parameters needed by the action above (to identify the resource)
            movie // The just created resource
        ); // for more ways to use it: https://www.code4it.dev/blog/createdAtRoute-createdAtAction
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Movie? movie = movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null)
            return NotFound();
        return Ok(movie);
    }
}