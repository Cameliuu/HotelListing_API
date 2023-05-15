using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HotelListing_API.Contracts;
using HotelListing_API.Features;
using HotelListing_API.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HotelListing_API.Repository;

public class AuthManager : IAuthManager
{
    public IConfiguration Configuration { get; }
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;

    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
    {
        Configuration = configuration;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<IdentityError>> Register(ApiUserModel model)
    {
        var user = _mapper.Map<ApiUser>(model);
        user.UserName = model.Email;

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "User");
        return result.Errors;

    }

    public async Task<AuthResponse> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        bool isValidUser = await _userManager.CheckPasswordAsync(user,model.Password);

        if (user is null || !isValidUser)
            return null;
        var token = await GenerateToken(user);

        return new AuthResponse
        {
            token = token,
            userId = user.Id
        };
    }

    public async Task<string> GenerateToken(ApiUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid ", user.Id),
        }.Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: Configuration["JwtSettings:Issuer"],
            audience: Configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(Configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}