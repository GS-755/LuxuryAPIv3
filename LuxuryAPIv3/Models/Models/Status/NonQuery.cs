namespace LuxuryAPIv3.Models.Status
{
    public class NonQuery
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public bool Result { get; set; }

        public NonQuery() { }
        public NonQuery(int Status, string Message, bool Result)
        {
            this.Status = Status;
            this.Message = Message;
            this.Result = Result;
        }
    }
}