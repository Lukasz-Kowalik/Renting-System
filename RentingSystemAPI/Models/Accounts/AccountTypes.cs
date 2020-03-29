using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
   public static class AccountTypes
    {
        public enum Name
        {
            Visitor,
            Customer,
            Worker,
            Admin,
        }
    }
}
