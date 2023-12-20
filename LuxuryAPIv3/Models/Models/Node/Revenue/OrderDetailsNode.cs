using LuxuryAPIv3.Models.Revenue;
using LuxuryAPIv3.Models.Status;
using System.Collections.Generic;

namespace LuxuryAPIv3.Models.Node.Revenue
{
    public class OrderDetailsNode
    {
        public OrderDetailsNode()
        {
            this.OrderDetails = null;
            this.State = null;
        }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public NonQuery[] State { get; set; }
    }
}