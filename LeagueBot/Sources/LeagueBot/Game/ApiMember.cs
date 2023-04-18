using LeagueBot.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Game
{
    public abstract class ApiMember<T> where T : IApi
    {
        protected T Api
        {
            get;
            private set;
        }
        public ApiMember(T api)
        {
            this.Api = api;
        }
    }
}
