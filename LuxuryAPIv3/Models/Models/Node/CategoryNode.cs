using LuxuryAPIv3.Models.Status;
using System.Collections.Generic;

namespace LuxuryAPIv3.Models.Node
{
    public class CategoryNode
    {
        public CategoryNode()
        {
            this.Category = null;
            this.State = null;
        }

        public IEnumerable<Category> Category { get; set; }
        public NonQuery[] State { get; set; }
    }
}