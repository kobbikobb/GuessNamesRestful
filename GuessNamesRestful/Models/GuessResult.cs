using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessNamesRestful.Models
{
    public class GuessResult
    {
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string GuessedName { get; set; }
    }
}