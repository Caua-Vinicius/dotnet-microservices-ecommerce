using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Features.Inventory.Commands.AddProductStock
{
    public class AddProductStockHandler : IRequestHandler<AddProductStockCommand>
    {
        private readonly InventoryDbContext _context;
        public AddProductStockHandler(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddProductStockCommand request, CancellationToken cancellationToken)
        {

            var product = await _context.Products
            .FirstOrDefaultAsync(product => product.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.ProductId} not found.");
            }

            var previousQuantity = product.StockQuantity;

            product.StockQuantity += request.QuantityToAdd;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.StockHistories.AddAsync(new StockHistory
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                PreviousQuantity = previousQuantity,
                NewQuantity = product.StockQuantity,
                Reason = "Stock Added",
                AdjustmentDate = DateTime.UtcNow
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}