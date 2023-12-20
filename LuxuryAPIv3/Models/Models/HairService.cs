namespace LuxuryAPIv3.Models
{
    public class HairService
    {
        public int IdSvc { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int IdCate { get; set; }

        public HairService() { }
        public HairService(int IdSvc, string Name, double Price, int IdCate)
        {
            this.IdSvc = IdSvc;
            this.Name = Name;
            this.Price = Price;
            this.IdCate = IdCate;
        }
    }
}