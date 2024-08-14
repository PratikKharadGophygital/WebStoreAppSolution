using Dapper;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Entities;
using WebStoreApp.Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperDbContext _dbConnection;
        public ProductRepository(DapperDbContext context)
        {
            _dbConnection = context;
        }

        public async Task<int> CreateProductAsync(ProductEntity productAdd)
        {
            var newId = 0;
            using (var connectionString = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Name", productAdd.Name);
                parameters.Add("@Code", productAdd.Code);
                parameters.Add("@Price", productAdd.Price);
                parameters.Add("@Quantity", productAdd.Quantity);
                parameters.Add("@CouponCode", productAdd.CouponCode);
                parameters.Add("@CouponAmount", productAdd.CouponAmount);

                try
                {
                    newId = await connectionString.ExecuteScalarAsync<int>("psp_ProductInsert", parameters);

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

        public async Task<PaginatedResult<ProductEntity>> GetPagedProductAsync(int pageNumber, int pageSize, string sortColumn, string sortOrder, string searchTerm, CancellationToken cancellationToken = default)
        {
            using (var connectionString = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@SortColumn", sortColumn);
                parameters.Add("@SortOrder", sortOrder);
                parameters.Add("@SearchTerm", searchTerm);

                // Directly await the asynchronous method
                var multi = await connectionString.QueryMultipleAsync("GetPagedProducts", parameters, commandType: CommandType.StoredProcedure, commandTimeout: 30);

                var products = (await multi.ReadAsync<ProductEntity>()).ToList();
                var totalRecords = await multi.ReadSingleAsync<int>();

                return new PaginatedResult<ProductEntity>
                {
                    Items = products,
                    TotalRecords = totalRecords
                };              
                
            }
        }

        public async Task<ProductEntity> GetProductByIdAsync(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductID", id);

                try
                {

                    var result = await connection.QuerySingleOrDefaultAsync<ProductEntity>(
                        "usp_GetRecordByProductId",
                        parameters,
                        commandType: CommandType.StoredProcedure

                    );

                    return result; // Return the result directly
                }
                catch (SqlException sqlEx)
                {
                    throw new ApplicationException("An error occurred while retrieving the product. Please try again later.", sqlEx);
                }
                catch (Exception ex)
                {

                    throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
                }
            }
        }

        public async Task<bool> UpdateProductAsync(ProductEntity model)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductID", model.ProductID);
                parameters.Add("@Name", model.Name);
                parameters.Add("@Code", model.Code);
                parameters.Add("@Price", model.Price);
                parameters.Add("@Quantity", model.Quantity);
                parameters.Add("@CouponCode", model.CouponCode);
                parameters.Add("@CouponAmount", model.CouponAmount);

                try
                {
                    var result = await connection.ExecuteScalarAsync<string>("usp_UpdateProduct", parameters, commandType: CommandType.StoredProcedure);

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
