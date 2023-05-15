using HotelListing_API.Features;
using HotelListing_API.Features.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing_API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(ApiUserModel model);
    Task<bool> Login(LoginModel model);
}