using HoteListing.API.Data;

namespace HoteListing.API.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        public new Task<Country> GetAsync(int? id);

    }

}
