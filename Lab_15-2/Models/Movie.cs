using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_15_2.Models
{
    public class Movie
    {
        private int id;
        private string title;
        private string category;
        private string director;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Category { get => category; set => category = value; }
        public string Director { get => director; set => director = value; }
    }
}
