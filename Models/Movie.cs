using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Required(ErrorMessage = "The Title is required!")]
        public string Title {get; set;} = String.Empty;
        [StringLength(20, ErrorMessage = "The Genre can not have over 20 characters")]
        public string? Genre {get; set;}
        [Required(ErrorMessage = "The Director is required!")]
        public string Director {get; set;}
        [Required(ErrorMessage = "The 'Duration' Field is required.")][Range(1,600, ErrorMessage = "The duration must be between 1 and 600!")]
        public string? Duration {get; set;}
    }
}