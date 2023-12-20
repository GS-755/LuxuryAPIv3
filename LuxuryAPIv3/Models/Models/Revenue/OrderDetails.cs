namespace LuxuryAPIv3.Models.Revenue
{
    public class OrderDetails
    {
        public double TotalPrice { get; set; }
        public int IDSvc { get; set; }
        public int IDOrder { get; set; }

        public OrderDetails() { }
        public OrderDetails(double TotalPrice, int IDSvc, int IDOrder)
        {
            this.TotalPrice = TotalPrice;
            this.IDSvc = IDSvc;
            this.IDOrder = IDOrder;
        }
    }
}