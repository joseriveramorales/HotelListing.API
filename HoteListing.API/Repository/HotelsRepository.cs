using HoteListing.API.Contracts;
using HoteListing.API.Data;

namespace HoteListing.API.Repository
{
    // The point of extending from both the implementation of GenericRepository
    // and the interface IHotelsRepository is that I can leave some implementations up 
    // to the Generic Repository and parciular rules to be overriden by the IHotelsRepository. 


    // This helps me to keep it DRY and also follow Open Closed principle since I dont expect to modify the GenericRepository anymore.

    // This also uses the Liskov substitution principle, since my hotels repository will always maintain the behaviros of the GenericRepository,
    // with slight modifications.
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingDBContext context) : base(context)
        {

        }
    }
}
