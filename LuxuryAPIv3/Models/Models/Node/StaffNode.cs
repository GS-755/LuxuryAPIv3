using LuxuryAPIv3.Models.Status;
using System.Collections.Generic;

namespace LuxuryAPIv3.Models.Node
{
    public class StaffNode
    {
        public StaffNode()
        {
            this.Staff = null;
            this.State = null;
        }

        public IEnumerable<Staff> Staff { get; set; }
        public NonQuery[] State { get; set; }
    }
}