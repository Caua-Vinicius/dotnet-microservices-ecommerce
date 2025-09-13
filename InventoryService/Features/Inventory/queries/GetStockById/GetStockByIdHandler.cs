using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Features.Inventory.Queries.GetStock
{
    public class GetStockByIdHandler : IRequestHandler<GetStockByIdQuery, Product>
    {
        private readonly InventoryDbContext _context;
        public GetStockByIdHandler(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            var productFetched = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == request.ProductId, cancellationToken);

            if (productFetched == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.ProductId} not found.");
            }

            return productFetched;
        }
    }
}