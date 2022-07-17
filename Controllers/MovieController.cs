
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();

    [HttpPost]
    public void Create([FromBody] Movie movie)
    {
        movies.Add(movie);
        Console.WriteLine(movie.Title);
        Console.WriteLine(movie.Director);
        Console.WriteLine(movie.Genre);
        Console.WriteLine(movie.Duration);
    }

    [HttpGet]
    public IEnumerable<Movie> GetAll()
    {
        return movies;
    }
}