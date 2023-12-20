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
    public class HairServicesController : ApiController
    {
        // GET: api/HairServices
        public HairServiceNode Get()
        {
            // Define node for export
            HairServiceNode node = new HairServiceNode();   

            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            HairService[] hairServices = HairServicesAdapter.GetAll();
            // Define data and state to node 
            if (hairServices == null)
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
                node.HairServices = hairServices;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/HairServices/5
        public HairServiceNode Get(int id)
        {
            // Define node for export
            HairServiceNode node = new HairServiceNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            HairService[] hairServices = HairServicesAdapter.GetItem(id);
            // Define data and state to node
            if (hairServices == null || hairServices.Length == 0)
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
                node.HairServices = hairServices;
            }

            // Export node to JSON
            return node;
        }

        // POST: api/HairServices
        [HttpPost]
        public HairServiceNode Post([FromBody] HairService hairService)
        {
            // Define node for export
            HairServiceNode node = new HairServiceNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Assign id for hairService
            hairService.IdSvc = HairServicesAdapter.GetCurrentId() + 1;
            try
            {
                // Execute Adapter method
                string rows_affected = HairServicesAdapter.InsertData(hairService);
                // Define data and state to node
                HairService[] addedService = HairServicesAdapter.GetItem(hairService.IdSvc);
                StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                node.HairServices = addedService;
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

        // PUT: api/HairServices/5
        [HttpPost]
        public HairServiceNode Put(int id, [FromBody] HairService hairService)
        {
            StatusAdapter.Clear();
            HairServiceNode node = new HairServiceNode();
            try
            {
                HairService foundHS = HairServicesAdapter.GetItem(id).FirstOrDefault();
                if(foundHS != null)
                {
                    string rows_affected = HairServicesAdapter.UpdateData(hairService);
                    HairService[] updatedSvc = HairServicesAdapter.GetItem(id).ToArray();
                    StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));

                    node.HairServices = updatedSvc;
                    node.State = StatusAdapter.CurrentStatus;
                }
                else
                {
                    StatusAdapter.AddItem(new NonQuery(404, "Not found", false));
                    node.State = StatusAdapter.CurrentStatus;
                }
            }
            catch(Exception ex)
            {
                StatusAdapter.AddItem(new NonQuery(500, ex.Message, false));
                node.State = StatusAdapter.CurrentStatus;
            }

            return node;
        }

        // DELETE: api/HairServices/5
        [HttpDelete]
        public NonQuery[] Delete(int id)
        {
            StatusAdapter.Clear();
            try
            {
                HairService foundHS = HairServicesAdapter.GetItem(id).FirstOrDefault();
                if (foundHS != null)
                {
                    string rows_affected = HairServicesAdapter.DeleteData(foundHS.IdSvc);
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
