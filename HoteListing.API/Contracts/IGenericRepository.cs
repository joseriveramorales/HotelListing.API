namespace HoteListing.API.Contracts
{
    /// <summary>
    ///     Generic Repository will be in charge of comunicating with the database,
    ///     accepts T for class
    /// </summary>
    public interface IGenericRepository<T> where T: class
    {
        Task<T> GetAsync(int? id);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task DeleteAsync(int? id);

        Task UpdateAsync(T entity);

        Task<bool> Exists(int id);
    }

}
