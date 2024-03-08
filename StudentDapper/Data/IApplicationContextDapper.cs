using Dapper;

namespace StudentDapper.Data
{
    public interface IApplicationContextDapper
    {
        public Task<IEnumerable<T>> GetDataAsync<T>(string query, DynamicParameters? dynamicParameters = null);

        public Task<T> GetDataSingleAsync<T>(string query, DynamicParameters? dynamicParameters = null);

        public Task<bool> ExecuteSqlAsync<T>(string query, DynamicParameters? dynamicParameters = null);
    }
}
