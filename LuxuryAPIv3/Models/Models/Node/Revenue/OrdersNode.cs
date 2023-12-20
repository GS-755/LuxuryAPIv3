using LuxuryAPIv3.Models.Revenue;
using LuxuryAPIv3.Models.Status;
using System.Collections.Generic;

namespace LuxuryAPIv3.Models.Node.Revenue
{
    public class OrdersNode
    {
        public OrdersNode()
        {
            this.Orders = null;
            this.State = null;
        }

        public IEnumerable<Orders> Orders { get; set; }
        public NonQuery[] State { get; set; }
    }
}