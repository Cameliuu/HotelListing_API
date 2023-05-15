using HotelListing_API.Features.Hotel;

namespace HotelListing_API.Features.Country;

public class GetCountryDetailsModel : BaseCountryModel
{
    public string Id { get; set; }

    public List<GetHotelModel> Hotels { get; set; }
}

