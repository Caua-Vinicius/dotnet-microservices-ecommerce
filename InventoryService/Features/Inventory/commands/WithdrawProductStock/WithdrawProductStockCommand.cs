using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;
using MediatR;

namespace InventoryService.Features.Inventory.Commands.WithdrawProductStock
{
    public class WithdrawProductStockCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public int QuantityToWithdraw { get; set; }
        public string Reason { get; set; } = string.Empty;

    }
}