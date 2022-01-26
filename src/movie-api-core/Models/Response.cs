using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie_api_core.Models
{
    public class Response
    {
        public Movie[] Movies { get; set; }
    }

    public class Movie
    {
        public int? Year { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string ImdbPosterUrl { get; set; }
        public string RottenTomatoesRating { get; set; }
        public string Plot { get; set; }
    }
}
