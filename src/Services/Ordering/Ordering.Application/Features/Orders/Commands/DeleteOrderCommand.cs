using MediatR;

namespace Ordering.Application.Features.Orders.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
