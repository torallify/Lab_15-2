using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_15_2.Models;
using Lab_15_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab_15_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IDAL dal;

        public RandomController(IDAL dalObject)
        {
            dal = dalObject;
        }


        [HttpGet]
        public IEnumerable<Movie> Get(string category = null)
        {
            if (category == null)
            {
                IEnumerable<Movie> Movies = dal.GetRandomMovie();
                return Movies; //serialize the parameter into JSON and return an Ok (20x)
            }
            else
            {
                IEnumerable<Movie> Movies = dal.GetRandomMovieByCategory(category);
                return Movies;
            }
        }
    }
}