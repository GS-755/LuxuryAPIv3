using System;
using System.Linq;
using System.Web.Http;
using LuxuryAPIv3.Models;
using LuxuryAPIv3.Adapters;
using LuxuryAPIv3.Models.Node;
using LuxuryAPIv3.Models.Status;
using LuxuryAPIv3.Adapters.Status;

namespace LuxuryAPIv3.Controllers
{
    public class StaffsController : ApiController
    {
        // GET: api/Staffs
        public StaffNode Get()
        {
            // Define node for export
            StaffNode node = new StaffNode();

            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Staff[] staffs = StaffAdapter.GetAll();
            // Define data and state to node 
            if (staffs == null)
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
                node.Staff = staffs;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/Staffs/5
        public StaffNode Get(int id)
        {
            // Define node for export
            StaffNode node = new StaffNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Staff[] staffs = StaffAdapter.GetItem(id);
            // Define data and state to node
            if (staffs == null || staffs.Length == 0)
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
                node.Staff = staffs;
            }

            // Export node to JSON
            return node;
        }

        // POST: api/Staffs
        [HttpPost]
        public StaffNode Post([FromBody] Staff staff)
        {
            // Define node for export
            StaffNode node = new StaffNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Assign id for staff
            staff.IDStaff = StaffAdapter.GetCurrentId() + 1;
            try
            {
                // Execute Adapter method
                string rows_affected = StaffAdapter.InsertData(staff);
                // Define data and state to node
                Staff[] addedStaff = StaffAdapter.GetItem(staff.IDStaff);
                StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                node.Staff = addedStaff;
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

        // PUT: api/Staffs/5
        [HttpPost]
        public StaffNode Put(int id, [FromBody] Staff staff)
        {
            StatusAdapter.Clear();
            StaffNode node = new StaffNode();
            try
            {
                Staff foundStaff = StaffAdapter.GetItem(id).FirstOrDefault();
                if (foundStaff != null)
                {
                    string rows_affected = StaffAdapter.UpdateData(staff);
                    Staff[] updatedStaff = StaffAdapter.GetItem(id).ToArray();
                    StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));

                    node.Staff = updatedStaff;
                    node.State = StatusAdapter.CurrentStatus;
                }
                else
                {
                    StatusAdapter.AddItem(new NonQuery(404, "Not found", false));
                    node.State = StatusAdapter.CurrentStatus;
                }
            }
            catch (Exception ex)
            {
                StatusAdapter.AddItem(new NonQuery(500, ex.Message, false));
                node.State = StatusAdapter.CurrentStatus;
            }

            return node;
        }

        // DELETE: api/Staffs/5
        public NonQuery[] Delete(int id)
        {
            StatusAdapter.Clear();
            try
            {
                Staff foundStaff = StaffAdapter.GetItem(id).FirstOrDefault();
                if (foundStaff != null)
                {
                    string rows_affected = StaffAdapter.DeleteData(foundStaff.IDStaff);
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
