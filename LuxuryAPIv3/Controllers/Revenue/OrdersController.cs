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
    public class OrdersController : ApiController
    {
        // GET: api/Orders
        public OrdersNode Get()
        {
            // Define node for export
            OrdersNode node = new OrdersNode();

            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Orders[] orders = OrdersAdapter.GetAll();
            // Define data and state to node 
            if (orders == null)
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
                node.Orders = orders;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/Orders/5
        public OrdersNode Get(int id)
        {
            // Define node for export
            OrdersNode node = new OrdersNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Orders[] orders = OrdersAdapter.GetItem(id);
            // Define data and state to node
            if (orders == null || orders.Length == 0)
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
                node.Orders = orders;
            }

            // Export node to JSON
            return node;
        }

        // POST: api/Orders
        [HttpPost]
        public OrdersNode Post([FromBody] Orders order)
        {
            // Define node for export
            OrdersNode node = new OrdersNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Assign id for order
            order.IDOrder = OrdersAdapter.GetCurrentId() + 1;
            try
            {
                // Execute Adapter method
                string rows_affected = OrdersAdapter.InsertData(order);
                // Define data and state to node
                Orders[] addedOrder = OrdersAdapter.GetItem(order.IDOrder);
                StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                node.Orders = addedOrder;
                node.State = StatusAdapter.CurrentStatus;

                // Export node to JSON
                return node;
            }
            catch (Exception ex)
            {
                // Define state to Node
                StatusAdapter.AddItem(new NonQuery(500, ex.Message, false));
                node.State = StatusAdapter.CurrentStatus;

                // Export node to JSON 
                return node;
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete]
        public NonQuery[] Delete(int id)
        {
            StatusAdapter.Clear();
            try
            {
                Orders foundOrder = OrdersAdapter.GetItem(id).FirstOrDefault();
                if (foundOrder != null)
                {
                    string rows_affected = OrdersAdapter.DeleteData(foundOrder.IDOrder);
                    StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                }
                else
                {
                    StatusAdapter.AddItem(new NonQuery(404, "Not found", false));
                }
            }
            catch (Exception ex)
            {
                StatusAdapter.AddItem(new NonQuery(500, ex.Message, false));
            }

            return StatusAdapter.CurrentStatus.ToArray();
        }
    }
}
