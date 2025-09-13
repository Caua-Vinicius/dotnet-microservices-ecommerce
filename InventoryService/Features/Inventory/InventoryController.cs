using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Features.Inventory.Commands.AddProductStock;
using InventoryService.Features.Inventory.Commands.CreateProduct;
using InventoryService.Features.Inventory.Commands.UpdateProduct;
using InventoryService.Features.Inventory.Queries.GetStock;
using InventoryService.Features.Inventory.Queries.GetStockHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Features.Inventory
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetStockById), new { id = productId }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            var products = await _mediator.Send(new GetStockQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(Guid id)
        {
            var product = await _mediator.Send(new GetStockByIdQuery { ProductId = id });
            return Ok(product);
        }

        [HttpGet("stockhistory")]
        public async Task<IActionResult> GetStockHistory()
        {
            var stockHistory = await _mediator.Send(new GetStockHistoryQuery { });
            return Ok(stockHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }

        [HttpPut("{id}/restock")]
        public async Task<IActionResult> RestockProduct(Guid id, [FromBody] int quantityToAdd)
        {
            await _mediator.Send(new AddProductStockCommand
            {
                ProductId = id,
                QuantityToAdd = quantityToAdd
            });
            return NoContent();
        }
    }
}