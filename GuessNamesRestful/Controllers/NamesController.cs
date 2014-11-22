using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuessNamesRestful.Models;

namespace GuessNamesRestful.Controllers
{
    public class NamesController : ApiController
    {
        private readonly GuessNamesContext guessNamesContext;

        public NamesController()
        {
            guessNamesContext = new GuessNamesContext();
        }

        public int Post(string fullName)
        {
            var name = Name.CreateName(fullName);

            guessNamesContext.Names.Add(name);
            guessNamesContext.SaveChanges();

            return name.Id;
        }

        public IEnumerable<Name> Get(int nameId)
        {
            return guessNamesContext.Names;
        }
    }
}
