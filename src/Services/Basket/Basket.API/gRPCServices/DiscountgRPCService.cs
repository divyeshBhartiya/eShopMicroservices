using Discount.gRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.gRPCServices
{
    public class DiscountgRPCService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountgRPCService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
