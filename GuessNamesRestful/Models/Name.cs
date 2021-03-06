﻿using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
            if(userName == null)
                throw new NullReferenceException("Username can not be null.");

            return new Guess()
            {
                Date = DateTime.Now,
                GuessedName = guessedName,
                Score = GetScore(guessedName),
                UserName = userName,
                ClientIp = GetClientIpAddress()
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

        public bool CanClientGuess()
        {
            var clientIp = GetClientIpAddress();

            var guessesToday = Guesses
                .Where(x => 
                    x.ClientIp == clientIp && x.Date.Date == DateTime.Today);

            return guessesToday.Count() < 10;
        }

        private string GetClientIpAddress()
        {
            var clinetForwardedIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(clinetForwardedIp))
                return clinetForwardedIp;

            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}