using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuessNamesRestful.Models
{
    public class GuessNamesContext : DbContext
    {
        public GuessNamesContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Guess> Guesses { get; set; }
        public DbSet<Name> Names { get; set; } 
    }
}