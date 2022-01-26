using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using movie_api_repo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace movie_api.Controllers
{
    [Authorize]
    [Route("api/get-movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public MoviesController(IConfiguration Config)
        {
            Configuration = Config;
        }

        // GET api/<MoviesController>/5
        [HttpGet("{imdbIds}")]
        public string Get(string imdbIds)
        {
            MovieService service = new(Configuration);

            return service.GetMovies(imdbIds);
        }

    }
}
