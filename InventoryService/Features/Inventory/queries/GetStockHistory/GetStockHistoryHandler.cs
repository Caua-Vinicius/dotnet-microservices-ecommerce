using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Features.Inventory.Queries.GetStockHistory
{
    public class GetStockHistoryHandler : IRequestHandler<GetStockHistoryQuery, IEnumerable<StockHistory>>
    {
        private readonly InventoryDbContext _context;

        public GetStockHistoryHandler(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockHistory>> Handle(GetStockHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.StockHistories.
            AsNoTracking()
            .ToListAsync(cancellationToken);
        }
    }
}