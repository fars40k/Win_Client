namespace Win_Dev.Data
{
    public interface INetworkClient
    {
        void Initialise();      
        void LogIn(string login, string password);

        void LogOut();
    }
}