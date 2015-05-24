namespace Twitter.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public int ReporterId { get; set; }
        public virtual User Reporter { get; set; }
        public int ReportedId { get; set; }
        public virtual Tweet Reported { get; set; }
    }
}
