using yourmusic.Context;

namespace yourmusic.Auth
{
    public interface IAuth
    {
        string Connection(ContextDatabase database, string username, string password);
    }
}