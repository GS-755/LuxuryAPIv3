namespace LuxuryAPIv3.Models
{
    public class Staff
    {
        public int IDStaff { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string DOB { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public double CurrentSalary { get; set; }

        public Staff() { }
        public Staff(int IDStaff, string FName, string LName, string Phone, string Address, double CurrentSalary)
        {
            this.IDStaff = IDStaff;
            this.FName = FName;
            this.LName = LName;
            this.Phone = Phone;
            this.Address = Address;
            this.CurrentSalary = CurrentSalary;
        }
        public Staff(int IDStaff, string FName, string LName, string DOB, string Phone, string Address, double CurrentSalary)
        {
            this.IDStaff = IDStaff;
            this.FName = FName;
            this.LName = LName;
            this.DOB = DOB;
            this.Phone = Phone;
            this.Address = Address;
            this.CurrentSalary = CurrentSalary;
        }
    }
}