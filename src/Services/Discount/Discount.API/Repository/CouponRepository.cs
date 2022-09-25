using Dapper;
using Discount.API.Entity;
using Npgsql;

namespace Discount.API.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            return coupon ?? new Coupon() { Amount = 0, Description = "No Coupon Available", ProductName = productName };
        }

        public async Task<Coupon> GetDiscountById(int id)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE Id = @Id", new { Id = id });
            return coupon ?? new Coupon() { Amount = 0, Description = "No Coupon Available", ProductName = "" };
        }

        public async Task<bool> Update(Coupon coupon)
        {
            await using var connection =
                new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));

            var result = await connection.ExecuteAsync(
                "Update Coupon Set ProductName = @ProductName,Amount = @Amount,Description = @Description WHERE Id = @Id",
                new { ProductName = coupon.ProductName.ToLower(), coupon.Amount, coupon.Description, coupon.Id });

            return result != 0;
        }

        public async Task<bool> Delete(int id)
        {
            await using var connection =
                new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));

            var result = await connection.ExecuteAsync(
                "DELETE FROM Coupon WHERE Id = @Id",
                new { id });

            return result != 0;
        }

        public async Task<bool> Add(Coupon coupon)
        {
            await using var connection =
                new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));

            var result = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName,Description,Amount) VALUES(@ProductName,@Description,@Amount)",
                new { ProductName = coupon.ProductName.ToLower(), coupon.Description, coupon.Amount });

            return result != 0;
        }
    }
}
