namespace SportCenter.Services.Auth
{
    public interface ILoginService
    {
        string Login(string username, string password);
    }
}
