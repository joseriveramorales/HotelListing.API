using HoteListing.API.Contracts;
using HoteListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HoteListing.API.Repository
{

    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDBContext _context;
        public CountriesRepository(HotelListingDBContext context) : base(context)
        {
            _context = context;
        }
        
        public new async Task<Country> GetAsync(int? id)
        {
            if (id is null) return null;
            var country = await _context.Countries
                .Include(country => country.Hotels)
                .FirstOrDefaultAsync(country => country.Id == id);
            return country;
        }
    }
}
