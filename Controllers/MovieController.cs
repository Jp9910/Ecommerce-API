
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private List<Movie> movies = new List<Movie>();

    [HttpPost]
    public void post([FromBody] Movie movie)
    {
        movies.Add(movie);
    }
}