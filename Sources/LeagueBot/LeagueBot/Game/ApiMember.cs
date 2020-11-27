using LeagueBot.Api;

namespace LeagueBot.Game
{
    public abstract class ApiMember<T> where T : IApi
    {
        public ApiMember(T api)
        {
            this.Api = api;
        }

        protected T Api
        {
            get;
            private set;
        }
    }
}