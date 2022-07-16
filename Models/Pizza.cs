// Tutorial: https://docs.microsoft.com/en-us/learn/modules/build-web-api-aspnet-core/5-exercise-add-data-store

namespace ContosoPizza.Models;

public class Pizza
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsGlutenFree { get; set; }
}