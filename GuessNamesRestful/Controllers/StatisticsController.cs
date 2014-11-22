using System.Linq;
using System.Web.Http;
using GuessNamesRestful.Models;

namespace GuessNamesRestful.Controllers
{
    public class StatisticsController : ApiController
    {
        private readonly GuessNamesContext guessNamesContext;

        public StatisticsController()
        {
            guessNamesContext = new GuessNamesContext();
        }

        public Statistics Get(int nameId)
        {
            var name = guessNamesContext.Names.Single(x => x.Id == nameId);

            return new Statistics
            {
                TotalTries = name.Guesses.Count,
                PartiallyCorrectTries = name.Guesses.Count(x => x.Score == Score.PartiallyCorrect),
                CorrectTries = name.Guesses.Count(x => x.Score == Score.Correct)
            };
        }
    }
}
