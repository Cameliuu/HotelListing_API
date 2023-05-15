using AutoMapper;
using HotelListing_API.Contracts;
using HotelListing_API.Features;
using HotelListing_API.Features.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing_API.Repository;

public class AuthManager : IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;

    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
    {
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

    public async Task<bool> Login(LoginModel model)
    {
     
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return default;
            var validPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            return validPassword;
        }
        catch (Exception e)
        {
            throw;
        }

        return false;
    }
}