using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuessNamesRestful.Models;

namespace GuessNamesRestful.Controllers
{
    public class GuessesController : ApiController
    {
        private readonly GuessNamesContext guessNamesContext;

        public GuessesController()
        {
            guessNamesContext = new GuessNamesContext();
        }

        public Score Post(int nameId, string userName, string guessedName)
        {
            var name = guessNamesContext.Names.Single(x => x.Id == nameId);
            var guess = name.MakeGuess(userName, guessedName);
            
            name.AddGuess(guess);
            guessNamesContext.SaveChanges();
            
            return guess.Score;
        }

        public IEnumerable<GuessResult> Get(int nameId, string userName)
        {
            var name = guessNamesContext.Names.Single(x => x.Id == nameId);
            return ToResult(name.Guesses.Where(x => x.UserName == userName));
        }

        public IEnumerable<GuessResult> Get(int nameId)
        {
            var name = guessNamesContext.Names.Single(x => x.Id == nameId);
            return ToResult(name.Guesses);
        }

        private IEnumerable<GuessResult> ToResult(IEnumerable<Guess> guesses)
        {
            return guesses.Select(x => new GuessResult()
            {
                Date = x.Date,
                GuessedName = x.GuessedName,
                UserName = x.UserName
            }).ToList();
        }
    }
}
