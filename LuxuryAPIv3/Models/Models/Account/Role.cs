namespace LuxuryAPIv3.Models.Account
{
    public class Role
    {
        public int IDRole { get; set; }
        public string Name { get; set; }

        public Role() { }
        public Role(int IDRole, string Name)
        {
            this.IDRole = IDRole;
            this.Name = Name;
        }
    }
}