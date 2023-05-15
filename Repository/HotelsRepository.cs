using HotelListing_API.Contracts;
using HotelListing_API.Features.Hotel;

namespace HotelListing_API.Repository;

public class HotelsRepository : GenericRepository<Hotel>,IHotelsRepository
{
    public HotelsRepository(AppDbContext context) : base(context)
    {
        
    }
}