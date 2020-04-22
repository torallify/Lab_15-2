using Dapper;
using Lab_15_2.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_15_2.Services
{
    public class DALSqlServer : IDAL
    {
        private string connectionString;

        public DALSqlServer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("movieDB");
        }

        public int CreateMovie(Movie m)
        {
            SqlConnection connection = null;
            string queryString = "INSERT INTO Movies (Title, Category, Description, Director)";
            queryString += " VALUES (@Title, @Category, @Description, @Director);";
            queryString += " SELECT SCOPE_IDENTITY();";
            int newId;

            try
            {
                connection = new SqlConnection(connectionString);
                newId = connection.ExecuteScalar<int>(queryString, m);
            }
            catch (Exception e)
            {
                newId = -1;
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return newId;
        }

        //TODO:finish
        public int DeleteMovieById(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string deleteCommand = "DELETE FROM Movies WHERE ID = @id";
            int rows = connection.Execute(deleteCommand, new { id = id });
            return rows;
        }

        public string[] GetMovieCategories()
        {
            SqlConnection connection = null;
            string queryString = "SELECT DISTINCT Category FROM Movies";
            IEnumerable<Movie> Movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                Movies = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            if (Movies == null)
            {
                return null;
            }
            else
            {
                string[] categories = new string[Movies.Count()];
                int count = 0;

                foreach (Movie m in Movies)
                {
                    categories[count] = m.Category;
                    count++;
                }

                return categories;
            }

        }

        public IEnumerable<Movie> GetMoviesAll()
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM Movies";
            IEnumerable<Movie> Movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                Movies = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return Movies;
        }

        public IEnumerable<Movie> GetMoviesByCategory(string category)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM Movies WHERE Category = @cat";
            IEnumerable<Movie> Movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                Movies = connection.Query<Movie>(queryString, new { cat = category });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return Movies;
        }

        public Movie GetMovieById(int id)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM Movies WHERE Id = @id";
            Movie movie = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movie = connection.QueryFirstOrDefault<Movie>(queryString, new { id = id });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return movie;
        }

        public IEnumerable<Movie> GetRandomMovie()
        {
            SqlConnection connection = null;
            string queryString = "SELECT TOP 1 * FROM Movies ORDER BY NEWID()";
            IEnumerable<Movie> Movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                Movies = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return Movies;
        }

        public IEnumerable<Movie> GetRandomMovieByCategory(string category)
        {
            SqlConnection connection = null;
            string queryString = "SELECT TOP 1 * FROM Movies WHERE Category = @cat ORDER BY NEWID()";
            IEnumerable<Movie> Movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                Movies = connection.Query<Movie>(queryString, new { cat = category });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return Movies;
        }
    }
}
