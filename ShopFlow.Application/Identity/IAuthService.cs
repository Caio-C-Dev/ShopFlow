namespace ShopFlow.Application.Identity;

public interface IAuthService
{
    Task<string> RegisterAsync(string nomeCompleto, string email, string password);
    Task<string> LoginAsync(string email, string password);
}