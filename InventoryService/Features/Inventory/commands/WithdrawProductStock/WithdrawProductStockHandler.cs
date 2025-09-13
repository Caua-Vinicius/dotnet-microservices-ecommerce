using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Features.Inventory.Commands.WithdrawProductStock
{
    public class WithdrawProductStockHandler : IRequestHandler<WithdrawProductStockCommand>
    {
        private readonly InventoryDbContext _context;

        public WithdrawProductStockHandler(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task Handle(WithdrawProductStockCommand request, CancellationToken cancellationToken)
        {

            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.ProductId} not found.");
            }

            if (product.StockQuantity < request.QuantityToWithdraw)
            {
                throw new InvalidOperationException($"Insufficient stock for product ID {request.ProductId}. Current stock: {product.StockQuantity}, Requested: {request.QuantityToWithdraw}");
            }

            var previousQuantity = product.StockQuantity;

            product.StockQuantity -= request.QuantityToWithdraw;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.StockHistories.AddAsync(new StockHistory
            {
                ProductId = product.Id,
                PreviousQuantity = previousQuantity,
                NewQuantity = product.StockQuantity,
                Reason = request.Reason,
                AdjustmentDate = DateTime.UtcNow,
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}