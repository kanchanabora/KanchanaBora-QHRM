using Dapper;
using QHRM.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace QHRM.Models
{
    public class ProductRepository : IProductInterface
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "SELECT * FROM Products";
                return await conn.QueryAsync<Products>(query);
            }
        }
        public async Task<string> AddProduct(Products product)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "INSERT INTO Products (Name,Price,Description,CreatedDate) VALUES (@Name,@Price,@Description,@CreatedDate); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await conn.ExecuteScalarAsync<string>(query, product);
            }
        }
		public async Task<bool> DeleteProduct(int id)
		{
			using (IDbConnection conn = Connection)
			{
				conn.Open();
				string query = "DELETE FROM Products WHERE Id = @Id";
				int rowsAffected = await conn.ExecuteAsync(query, new { Id = id });
				return rowsAffected > 0;
			}
		}
        public async Task<Products> GetProductById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE Id = @Id";
                return await conn.QuerySingleOrDefaultAsync<Products>(query, new { Id = id });
            }
        }
        public async Task<bool> UpdateProduct(int id, Products product)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "UPDATE Products SET Name = @Name, Price = @Price,Description=@Description,CreatedDate=@CreatedDate WHERE Id = @id";
                int rowsAffected = await conn.ExecuteAsync(query, product);
                return rowsAffected > 0;
            }
        }
    }
}
