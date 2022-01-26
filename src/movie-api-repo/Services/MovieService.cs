using Microsoft.Extensions.Configuration;
using movie_api_core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace movie_api_repo.Services
{
    public class MovieService
    {
        public string ApiKey { get; set; }
        public MovieService(IConfiguration Config)
        {
            ApiKey = Config.GetSection("OmdbapiSettings").GetSection("ApiKey").Value; 
        }

        public string GetMovies(string MovieImdbIds)
        {
            string[] ids = MovieImdbIds.Split("-");

            List<Movie> moviesObjects = GetMovieObjects(ids);

            Response response = new Response()
            {
                Movies = moviesObjects.ToArray()
            };

            return JsonSerializer.Serialize(response);


        }

        private List<Movie> GetMovieObjects(string[] ids)
        {
            List<Movie> movies = new();
            for (int i =0;i < ids.Length; i++)
            {
                string json = string.Empty;
                string url = @"http://www.omdbapi.com/?apikey=" + ApiKey + "&i=" + ids[i];

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }

                OmdbResponse omdbResponse =
                JsonSerializer.Deserialize<OmdbResponse>(json);

                movies.Add(
                    new Movie()
                    {
                        ImdbId = ids[i],
                        Director =!string.IsNullOrEmpty( omdbResponse.Director) ? omdbResponse.Director : null,
                        Title = !string.IsNullOrEmpty(omdbResponse.Title) ? omdbResponse.Title : null,
                        ImdbPosterUrl = !string.IsNullOrEmpty(omdbResponse.Poster) ? omdbResponse.Poster : null,
                        Plot= !string.IsNullOrEmpty(omdbResponse.Plot) ? omdbResponse.Plot : omdbResponse.Plot,
                        RottenTomatoesRating= GetRottenTomatoesRatings(ref omdbResponse),
                        Year = !string.IsNullOrEmpty(omdbResponse.Year) ? Convert.ToInt32(omdbResponse.Year) : null
                    }
                    );
            }
            return movies;
        }

        private string GetRottenTomatoesRatings(ref OmdbResponse omdbResponse)
        {
            string rating = null;
            for(int i = 0; i < omdbResponse.Ratings.Count; i++)
            {
                if (omdbResponse.Ratings[i].Source == "Rotten Tomatoes")
                {
                    rating = omdbResponse.Ratings[i].Value;
                }
            }

            return rating;
        }
    }
}
