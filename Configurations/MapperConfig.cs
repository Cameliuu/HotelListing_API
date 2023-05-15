using AutoMapper;
using HotelListing_API.Features;
using HotelListing_API.Features.Country;
using HotelListing_API.Features.Hotel;

namespace HotelListing_API.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Country, CreateCountryModel>().ReverseMap();
        CreateMap<Country, GetCountryModel>().ReverseMap();
        CreateMap<Country, GetCountryDetailsModel>().ReverseMap();
        CreateMap<Country, UpdateCountryModel>().ReverseMap();
        CreateMap<Hotel, GetHotelModel>().ReverseMap();
        CreateMap<Hotel, CreateHotelModel>().ReverseMap();
        CreateMap<ApiUser, ApiUserModel>().ReverseMap();
    }
}