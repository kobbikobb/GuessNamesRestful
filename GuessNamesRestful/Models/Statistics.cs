using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessNamesRestful.Models
{
    public class Statistics
    {
        public int TotalTries { get; set; }
        public int PartiallyCorrectTries { get; set; }
        public int CorrectTries { get; set; }
    }
}