using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace GuessNamesRestful.Models
{
    public enum Score
    {
        Wrong,
        PartiallyCorrect,
        Correct
    }

    public class Name
    {
        public int Id { get; set; }
        public string FirstNameHash { get; set; }
        public string FullNameHash { get; set; }
        public virtual List<Guess> Guesses { get; set; } 

        public static Name CreateName(string fullName)
        {
            var firstName = GetFirstName(fullName.ToLower());

            return new Name()
            {
                FirstNameHash = Crypto.HashPassword(firstName),
                FullNameHash = Crypto.HashPassword(fullName.ToLower())
            };
        }

        public Guess MakeGuess(string userName, string guessedName)
        {
            return new Guess()
            {
                Date = DateTime.Now,
                GuessedName = guessedName,
                Score = GetScore(guessedName),
                UserName = userName
            };
        }

        private Score GetScore(string guessedName)
        {
            if(Crypto.VerifyHashedPassword(FullNameHash, guessedName.ToLower()))
                return Score.Correct;
            if (Crypto.VerifyHashedPassword(FirstNameHash, GetFirstName(guessedName.ToLower())))
                return Score.PartiallyCorrect;
            return Score.Wrong;
        }

        private static string GetFirstName(string fullName)
        {
            return fullName.Split(' ').First();
        }

        public void AddGuess(Guess guess)
        {
            Guesses.Add(guess);
        }
    }
}