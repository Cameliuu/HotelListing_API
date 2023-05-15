using System.ComponentModel.DataAnnotations;
using HotelListing_API.Features.Users;

namespace HotelListing_API.Features;

public class ApiUserModel : LoginModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
   
}