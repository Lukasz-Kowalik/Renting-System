using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.DTOs.Response
{
    public class RentedItemsResponse
    {
        public int RentId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime RentTime { get; set; }
        public DateTime WhenShouldBeReturned { get; set; }
        public DateTime? RentReturnTime { get; set; }
    }
}