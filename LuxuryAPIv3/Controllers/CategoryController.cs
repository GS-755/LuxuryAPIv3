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
    public class CategoryController : ApiController
    {
        // GET: api/Category
        public CategoryNode Get()
        {
            // Define node for export
            CategoryNode node = new CategoryNode();

            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Category[] categories = CategoryAdapter.GetAll();
            // Define data and state to node 
            if (categories == null)
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
                node.Category = categories;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/Category/5
        public CategoryNode Get(int id)
        {
            // Define node for export
            CategoryNode node = new CategoryNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Category[] categories = CategoryAdapter.GetItem(id);
            // Define data and state to node
            if (categories == null || categories.Length == 0)
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
                node.Category = categories;
            }

            // Export node to JSON
            return node;
        }

        // POST: api/Category
        [HttpPost]
        public CategoryNode Post([FromBody] Category category)
        {
            // Define node for export
            CategoryNode node = new CategoryNode();
            // Wipe state list
            StatusAdapter.Clear();
            try
            {
                // Execute Adapter method
                string rows_affected = CategoryAdapter.InsertData(category);
                // Define data and state to node
                Category[] addedCategory = CategoryAdapter.GetItem(Convert.ToInt32(category.IdCate));
                StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                node.Category = addedCategory;
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

        // PUT: api/Category/5
        [HttpPost]
        public CategoryNode Put(int id, [FromBody] Category category)
        {
            // Define node for export
            CategoryNode node = new CategoryNode();
            // Wipe state list
            StatusAdapter.Clear();
            try
            {
                // Find category by id
                Category foundCate = CategoryAdapter.GetItem(id).FirstOrDefault();
                if (foundCate != null)
                {
                    // Execute Adapter method
                    string rows_affected = CategoryAdapter.UpdateData(category);
                    // Verify if object is saved to database
                    Category[] updatedCate = CategoryAdapter.GetItem(id).ToArray();
                    StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                    // Define data & state to Node 
                    node.Category = updatedCate;
                    node.State = StatusAdapter.CurrentStatus;
                }
                else
                {
                    // Define state to Node 
                    StatusAdapter.AddItem(new NonQuery(404, "Not found", false));
                    node.State = StatusAdapter.CurrentStatus;
                }
            }
            catch (Exception ex)
            {
                // Define state to Node 
                StatusAdapter.AddItem(new NonQuery(500, ex.Message, false));
                node.State = StatusAdapter.CurrentStatus;
            }

            // Export node to JSON
            return node;
        }

        // DELETE: api/Category/5
        [HttpDelete]
        public NonQuery[] Delete(int id)
        {
            // Wipe state list
            StatusAdapter.Clear();
            try
            {
                // Find category by id
                Category foundCate = CategoryAdapter.GetItem(id).FirstOrDefault();
                if (foundCate != null)
                {
                    // Execute Adapter method
                    string rows_affected = CategoryAdapter.DeleteData(Convert.ToInt32(foundCate.IdCate));
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

            // Return state to JSON
            return StatusAdapter.CurrentStatus.ToArray();
        }
    }
}
