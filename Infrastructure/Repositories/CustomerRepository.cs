using Dapper;
using Infrastructure.Data;
using System.Data;
using System.Data.SqlClient;
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

        public async Task<int> CreateUserAsync(Customer customer)
        {
            var newId = 0;
            using (var connectionString = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@FirstName", customer.FirstName);
                parameters.Add("@LastName", customer.LastName);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@UserName", customer.UserName);
                parameters.Add("@Address", customer.Address);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);

                try
                {
                    newId = await connectionString.ExecuteScalarAsync<int>("usp_InsertRecord", parameters);

                    if (newId == 0)
                    {
                        Console.WriteLine("Record is not insert in database");
                    }
                    else
                    {
                        return newId;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
                catch (Exception ex)
                {

                    throw;
                }


            }

            return newId;
        }

        public async Task<(IEnumerable<Customer> Users, int TotalRecords)> GetPagedUsersAsync(int pageNumber, int pageSize, string sortColumn, string sortOrder, string searchTerm)
        {
            using (var connectionString = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@SortColumn", sortColumn);
                parameters.Add("@SortOrder", sortOrder);
                parameters.Add("@SearchTerm", searchTerm);

                using (var multi = await connectionString.QueryMultipleAsync("GetPagedUsers", parameters, commandType: CommandType.StoredProcedure))
                {
                    var customers = await multi.ReadAsync<Customer>();
                    var totalRecords = await multi.ReadSingleAsync<int>();

                    return (customers, totalRecords);

                }

            }
        }

        public async Task<Customer> GetUserByIdAsync(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                try
                {

                    var result = await connection.QuerySingleOrDefaultAsync<Customer>(
                        "usp_GetRecordByUserId",
                        parameters,
                        commandType: CommandType.StoredProcedure

                    );

                    return result; // Return the result directly
                }
                catch (SqlException sqlEx)
                {
                    // Log detailed SQL exception
                    // _logger.LogError(sqlEx, "SQL Error occurred while retrieving user by ID.");
                    throw new ApplicationException("An error occurred while retrieving the user. Please try again later.", sqlEx);
                }
                catch (Exception ex)
                {
                    // Log general exception
                    // _logger.LogError(ex, "General Error occurred while retrieving user by ID.");
                    throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
                }
            }
        }

        public async Task<bool> UpdateUserAsync(Customer customer)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", customer.Id);
                parameters.Add("@FirstName", customer.FirstName);
                parameters.Add("@LastName", customer.LastName);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@UserName", customer.UserName);
                parameters.Add("@Address", customer.Address);
                parameters.Add("@PhoneNumber", customer.PhoneNumber);

                try
                {
                    var result = await connection.ExecuteScalarAsync<string>("usp_UpdateUser",parameters,commandType: CommandType.StoredProcedure);

                    if (result == "Success")
                    {
                        return true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return false;
        }
    }
}
