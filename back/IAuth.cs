namespace yourmusic.Auth
{
    public interface IAuth
    {
        string Connection(string username, string password);
    }
}