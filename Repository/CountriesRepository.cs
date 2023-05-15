using HotelListing_API.Contracts;
using HotelListing_API.Features.Country;
using Microsoft.EntityFrameworkCore;

namespace HotelListing_API.Repository;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly AppDbContext _context;
    public CountriesRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }
    public async Task<Country> GetDetails(int id)
    {
        return await _context.Countries.Include(q => q.Hotels)
            .FirstOrDefaultAsync(q => q.Id == id);
    }
}