using Dapper;
using Infrastructure.Data;
using System.Data;
using WebStoreApp.Domain.Entities;
using WebStoreApp.Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperDbContext _context;

        public CustomerRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            const string storedProcedure = "GetAllCustomers";

            using (var connectionString = _context.CreateConnection())
            {
                return await connectionString.QueryAsync<Customer>(storedProcedure,commandType: CommandType.StoredProcedure);
            }
        }

    }
}
