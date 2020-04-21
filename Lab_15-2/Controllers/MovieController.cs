﻿using System;
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
    public class MovieController : ControllerBase
    {
        private IDAL dal;

        public MovieController(IDAL dalObject)
        {
            dal = dalObject;
        }

        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            int result = dal.DeleteMovieById(id);
            if (result < 0)
            {
                return new { success = true };
            }
            else
            {
                return new { success = false };
            }
        }

        [HttpGet("{id}")]
        public Movie GetSingleMovie(int id)
        {
            Movie Mov = dal.GetMovieById(id);
            return Mov; //serialize the parameter into JSON and return an Ok (20x)
        }

        [HttpGet]
        public IEnumerable<Movie> Get(string category = null)
        {
            if (category == null)
            {
                IEnumerable<Movie> Movies = dal.GetMoviesAll();
                return Movies; //serialize the parameter into JSON and return an Ok (20x)
            }
            else
            {
                IEnumerable<Movie> Movies = dal.GetMoviesByCategory(category);
                return Movies;
            }
        }

        [HttpGet("categories")]
        public string[] GetCategories()
        {
            return dal.GetMovieCategories();
        }

        [HttpPost]
        public Object Post(Movie m)
        {
            int newId = dal.CreateMovie(m);
            if (newId < 0)
            {
                return new { success = false };
            }

            return new { status = true, id = newId };
        }

    }
}