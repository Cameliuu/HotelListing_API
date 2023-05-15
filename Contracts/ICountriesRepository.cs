using HotelListing_API.Features.Country;

namespace HotelListing_API.Contracts;

public interface ICountriesRepository : IGenericRepository<Country>
{
    Task<Country> GetDetails(int id);
}