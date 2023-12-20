using System;
using System.Linq;
using System.Web.Http;
using LuxuryAPIv3.Models.Status;
using LuxuryAPIv3.Models.Account;
using LuxuryAPIv3.Adapters.Status;
using LuxuryAPIv3.Adapters.Account;
using LuxuryAPIv3.Models.Node.Account;

namespace LuxuryAPIv3.Controllers.Account
{
    public class AccountsController : ApiController
    {
        // GET: api/Accounts
        public AccountsNode Get()
        {
            // Define node for export
            AccountsNode node = new AccountsNode();

            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Accounts[] accounts = AccountsAdapter.GetAll();
            // Define data and state to node 
            if (accounts == null)
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
                node.Accounts = accounts;
            }

            // Export node to JSON
            return node;
        }

        // GET: api/Accounts/5
        public AccountsNode Get(string UserName)
        {
            // Define node for export
            AccountsNode node = new AccountsNode();
            // Wipe state list
            StatusAdapter.Clear();
            // Execute Adapter method
            Accounts[] accounts = AccountsAdapter.GetItem(UserName);
            // Define data and state to node
            if (accounts == null || accounts.Length == 0)
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
                node.Accounts = accounts;
            }

            // Export node to JSON
            return node;
        }

        // POST: api/Accounts
        [System.Web.Http.HttpPost]
        public AccountsNode Post([FromBody] Accounts account)
        {
            // Define node for export
            AccountsNode node = new AccountsNode();
            // Wipe state list
            StatusAdapter.Clear();
            try
            {
                // Execute Adapter method
                string rows_affected = AccountsAdapter.InsertData(account);
                // Define data and state to node
                Accounts[] addedAccount = AccountsAdapter.GetItem(account.UserName);
                StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));
                node.Accounts = addedAccount;
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
        [System.Web.Http.HttpPost]
        // PUT: api/Accounts/5
        public AccountsNode Put(int id, [FromBody] Accounts account)
        {
            StatusAdapter.Clear();
            AccountsNode node = new AccountsNode();
            try
            {
                Accounts foundAccount = AccountsAdapter.GetItem(account.UserName).FirstOrDefault();
                if (foundAccount != null)
                {
                    string rows_affected = AccountsAdapter.UpdateData(account);
                    Accounts[] updatedAccount = AccountsAdapter.GetItem(account.UserName).ToArray();
                    StatusAdapter.AddItem(new NonQuery(200, rows_affected, true));

                    node.Accounts = updatedAccount;
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
    }
}
