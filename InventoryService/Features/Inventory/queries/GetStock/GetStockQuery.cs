using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;
using MediatR;

namespace InventoryService.Features.Inventory.Queries.GetStock
{
    public class GetStockQuery : IRequest<IEnumerable<Product>>
    {
    }
}