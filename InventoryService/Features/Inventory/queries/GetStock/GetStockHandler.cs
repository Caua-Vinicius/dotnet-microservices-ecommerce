
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Features.Inventory.Queries.GetStock
{
    public class GetStockHandler : IRequestHandler<GetStockQuery, IEnumerable<Product>>
    {
        private readonly InventoryDbContext _context;
        public GetStockHandler(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        }
    }
}