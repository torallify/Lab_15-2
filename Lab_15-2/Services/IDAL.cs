using Lab_15_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_15_2.Services
{
    public interface IDAL
    {
        Movie GetMovieById(int id);
        string[] GetMovieCategories();
        IEnumerable<Movie> GetMoviesAll();
        IEnumerable<Movie> GetMoviesByCategory(string category);
        //int UpdateMovieById(Movie mrod);
        int CreateMovie(Movie m);
        int DeleteMovieById(int id);
    }
}
