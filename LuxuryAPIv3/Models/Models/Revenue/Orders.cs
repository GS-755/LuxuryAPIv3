namespace LuxuryAPIv3.Models.Revenue
{
    public class Orders
    {
        public int IDOrder { get; set; }
        public string DateOrder { get; set; }
        public int IDStaff { get; set; }

        public Orders() { }
        public Orders(int IDOrder, string DateOrder, int IDStaff)
        {
            this.IDOrder = IDOrder;
            this.DateOrder = DateOrder;
            this.IDStaff = IDStaff;
        }
    }
}