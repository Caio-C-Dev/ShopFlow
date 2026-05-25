using Microsoft.AspNetCore.Identity;
using ShopFlow.Application.Identity;

namespace ShopFlow.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwtService;

    public AuthService(UserManager<ApplicationUser> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterAsync(string nomeCompleto, string email, string password)
    {
        var userExists = await _userManager.FindByEmailAsync(email);
        if (userExists is not null)
            throw new InvalidOperationException("Email já cadastrado.");

        var user = new ApplicationUser
        {
            NomeCompleto = nomeCompleto,
            Email = email,
            UserName = email
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao criar usuário: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "Customer");

        var roles = await _userManager.GetRolesAsync(user);
        return _jwtService.GenerateToken(user, roles);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new InvalidOperationException("Credenciais inválidas.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
            throw new InvalidOperationException("Credenciais inválidas.");

        var roles = await _userManager.GetRolesAsync(user);
        return _jwtService.GenerateToken(user, roles);
    }
}