
using InventoryService.Domain.Entities;
using MediatR;

namespace InventoryService.Features.Inventory.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

    }
}