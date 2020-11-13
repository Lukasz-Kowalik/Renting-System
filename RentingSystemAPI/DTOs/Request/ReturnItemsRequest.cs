using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.DTOs.Request
{
    public class ReturnItemsRequest
    {
        public int RentId { get; set; }
        public int ItemId { get; set; }
    }
}