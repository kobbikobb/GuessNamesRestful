using System;

namespace GuessNamesRestful.Models
{
    public class Guess
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string GuessedName { get; set; }
        public Score Score { get; set; }
    }
}