using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace InventoryService.Features.Inventory.Commands.WithdrawProductStock
{
    public class WithdrawProductStockCommandValidator : AbstractValidator<WithdrawProductStockCommand>
    {
        public WithdrawProductStockCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.QuantityToWithdraw).GreaterThan(0).WithMessage("QuantityToWithdraw must be greater than zero.");
        }
    }
}