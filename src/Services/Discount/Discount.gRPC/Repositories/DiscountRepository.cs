using Dapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.gRPC.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE LOWER(ProductName) = @ProductName", new { ProductName = productName.ToLower() });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync
                    ("INSERT INTO Coupon " +
                    "(ProductName, Description, Amount) " +
                    "VALUES (@ProductName, @Description, @Amount)",
                     new { coupon.ProductName, coupon.Description, coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, " +
                    "Description = @Description, " +
                    "Amount = @Amount WHERE Id = @Id",
                    new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }
    }
}