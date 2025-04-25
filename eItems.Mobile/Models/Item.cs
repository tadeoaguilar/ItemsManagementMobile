using System;

namespace eItems.Mobile.Models;

public class Item
{
        public Guid Id { get; set; }
        public string AssetCD { get; set; } = default!;
        public string AssetImage { get; set; } = default!;
        public int SubNumber { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        

        public string? LocationName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? CostCenterName { get; set; }
}
