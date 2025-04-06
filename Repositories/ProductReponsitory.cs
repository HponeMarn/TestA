using Dapper;
using TestA.Data;
using TestA.Model;

namespace TestA.Repositories
{
    public class ProductReponsitory
    {
        private readonly DapperContext _dapperContext;
        public ProductReponsitory(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ProductModel>> GetAllProduct()
        {
            var quer = @"SELECT * FROM Products ORDER BY CreatedDate DESC";
            using(var connection=_dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<ProductModel>(quer);
            }
        }
    }
}
