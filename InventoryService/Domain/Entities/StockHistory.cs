using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Domain.Entities
{
    public class StockHistory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int PreviousQuantity { get; set; }
        public int NewQuantity { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime AdjustmentDate { get; set; }

        public Product? Product { get; set; }
    }
}