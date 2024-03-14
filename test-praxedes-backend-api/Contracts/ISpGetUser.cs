namespace test_praxedes_backend_api.Contracts
{
    public interface ISpGetUser
    {
        Task<Models.User> Execute(string documentNumber);
    }
}