using LuxuryAPIv3.Models.Status;
using System.Collections.Generic;

namespace LuxuryAPIv3.Adapters.Status
{
    public class StatusAdapter
    {
        private static List<NonQuery> statusList = new List<NonQuery>();

        public static List<NonQuery> StatusList
        {
            get { return statusList; }
        }
        public static void AddItem(NonQuery status)
        {
            statusList.Add(status);
        }
        public static void Clear()
        {
            statusList.Clear();
        }
        public static NonQuery[] CurrentStatus
        {
            get { return StatusList.ToArray(); }
        }
    }
}