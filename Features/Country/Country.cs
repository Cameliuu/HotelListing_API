namespace HotelListing_API.Features.Country;

public class Country : BaseCountryModel
{
    public int Id { get; set; }
  
    public virtual ICollection<Hotel.Hotel> Hotels { get; set; }
}