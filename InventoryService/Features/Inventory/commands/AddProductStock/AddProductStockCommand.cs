using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;
using MediatR;

namespace InventoryService.Features.Inventory.Commands.AddProductStock
{
    public class AddProductStockCommand: IRequest
    {
        public Guid ProductId { get; set; }
        public int QuantityToAdd { get; set; }
    }
}