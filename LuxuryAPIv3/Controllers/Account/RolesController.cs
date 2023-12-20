using System.Web.Http;
using LuxuryAPIv3.Models.Status;
using LuxuryAPIv3.Models.Account;
using LuxuryAPIv3.Adapters.Status;
using LuxuryAPIv3.Adapters.Account;
using LuxuryAPIv3.Models.Node.Account;

namespace LuxuryAPIv3.Controllers
{
    public class RolesController : ApiController
    {
        // GET: api/Roles
        public RoleNode Get()
        {
            // Define node for export
            RoleNode node = new RoleNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Role[] roles = RolesAdapter.GetAll();
            // Define data and state to node 
            if (roles == null)
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
                node.Role = roles;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/Roles/5
        public RoleNode Get(int id)
        {
            // Define node for export
            RoleNode node = new RoleNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Role[] roles = RolesAdapter.GetItem(id);
            // Define data and state to node
            if (roles == null || roles.Length == 0)
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
                node.Role = roles;
            }

            // Export node to JSON
            return node;
        }
    }
}
