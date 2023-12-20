namespace LuxuryAPIv3.Models.Account
{
    public class Accounts
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IDRole { get; set; }
        public int IDStaff { get; set; }

        public Accounts() { }
        public Accounts(string UserName, string Password, int IDRole, int IDStaff)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.IDRole = IDRole;
            this.IDStaff = IDStaff;
        }
    }
}