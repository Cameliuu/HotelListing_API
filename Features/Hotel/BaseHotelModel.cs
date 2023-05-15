using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing_API.Features.Hotel;

public abstract class BaseHotelModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
    [Required]
    [Range(1,int.MaxValue)]
    [ForeignKey(nameof(CountryId))]
    public int CountryId { get; set; }
}