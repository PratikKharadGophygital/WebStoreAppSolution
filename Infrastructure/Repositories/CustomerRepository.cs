using Dapper;
using Infrastructure.Data;
using System.Data;

using WebStoreApp.Domain.Entities;
using WebStoreApp.Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperDbContext _dbConnection;
        

        public CustomerRepository(DapperDbContext context)
        {
            _dbConnection = context;
        }

        public async Task<(IEnumerable<Customer> , Int64 TotalCount)> GetAllAsync(Int64 pageNumber, int pageSize, string searchTerm, string sortColumn, string sortDirection)
        {
            const string storedProcedure = "GetAllCustomers";

            using (var connectionString = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@SearchTerm", searchTerm ?? (object)DBNull.Value);
                parameters.Add("@SortColumn", sortColumn);
                parameters.Add("@SortDirection", sortDirection);

                using (var multi = await connectionString.QueryMultipleAsync("GetAllCustomers", parameters, commandType: CommandType.StoredProcedure))
                {
                    var customers = multi.Read<Customer>().ToList();
                    var totalCount = multi.ReadSingle<Int64>();

                    return (customers,totalCount);

                }
              
            }
        }

    }
}
