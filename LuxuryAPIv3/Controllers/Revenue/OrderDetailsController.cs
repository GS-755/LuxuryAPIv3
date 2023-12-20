using System;
using System.Linq;
using System.Web.Http;
using LuxuryAPIv3.Models.Status;
using LuxuryAPIv3.Models.Revenue;
using LuxuryAPIv3.Adapters.Status;
using LuxuryAPIv3.Adapters.Revenue;
using LuxuryAPIv3.Models.Node.Revenue;

namespace LuxuryAPIv3.Controllers.Revenue
{
    public class OrderDetailsController : ApiController
    {
        // GET: api/OrderDetails
        public OrderDetailsNode Get()
        {
            // Define node for export
            OrderDetailsNode node = new OrderDetailsNode();

            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            OrderDetails[] orderDetails = OrderDetailsAdapter.GetAll();
            // Define data and state to node 
            if (orderDetails == null)
            {
                node.State = new NonQuery[]
                {
                    new NonQuery(204, "No content", false)
                };
            }
            else
            {
                node.State = new NonQuery[]
                {
                    new NonQuery(200, "GET Ok", true)
                };
                node.OrderDetails = orderDetails;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/OrderDetails/5
        public OrderDetailsNode Get(int id)
        {
            // Define node for export
            OrderDetailsNode node = new OrderDetailsNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            OrderDetails[] orderDetails = OrderDetailsAdapter.GetItem(id);
            // Define data and state to node
            if (orderDetails == null || orderDetails.Length == 0)
            {
                node.State = new NonQuery[]
                {
                    new NonQuery(404, "Not found", false)
                };
            }
            else
            {
                node.State = new NonQuery[]
                {
                    new NonQuery(200, "GET Ok", true)
                };
                node.OrderDetails = orderDetails;
            }

            // Export node to JSON
            return node;
        }

        // POST: api/OrderDetails
        [HttpPost]
        public NonQuery[] Post([FromBody] OrderDetails orderDetails)
        {
            // Wipe state list
            StatusAdapter.Clear();
            try
            {
                // Execute Adapter method
                string rows_affected = OrderDetailsAdapter.InsertData(orderDetails);
                StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));

                // Export state to JSON
                return StatusAdapter.CurrentStatus.ToArray();
            }
            catch (Exception ex)
            {
                // Define state to Node
                StatusAdapter.AddItem(new NonQuery(500, ex.Message, false));

                // Export state to JSON
                return StatusAdapter.CurrentStatus.ToArray();
            }
        }
    }
}
