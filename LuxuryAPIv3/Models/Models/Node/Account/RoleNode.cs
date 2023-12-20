using LuxuryAPIv3.Models.Account;
using LuxuryAPIv3.Models.Status;
using System.Collections.Generic;

namespace LuxuryAPIv3.Models.Node.Account
{
    public class RoleNode
    {
        public RoleNode()
        {
            this.Role = null;
            this.State = null;
        }

        public IEnumerable<Role> Role { get; set; }
        public NonQuery[] State { get; set; }
    }
}