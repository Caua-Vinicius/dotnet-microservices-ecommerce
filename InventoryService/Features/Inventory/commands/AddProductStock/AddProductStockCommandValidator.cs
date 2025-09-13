using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace InventoryService.Features.Inventory.Commands.AddProductStock
{
    public class AddProductStockCommandValidator : AbstractValidator<AddProductStockCommand>
    {
        public AddProductStockCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.QuantityToAdd).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}