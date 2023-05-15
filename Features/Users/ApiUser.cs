using Microsoft.AspNetCore.Identity;

namespace HotelListing_API.Features;

public class ApiUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}