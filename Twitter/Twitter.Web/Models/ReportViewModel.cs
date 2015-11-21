using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Web.Models
{
    public class ReportViewModel
    {
        public int Id { get; set; }

        [DisplayName("Report")]
        [MinLength(10)]
        [Required]
        public string Content { get; set; }

        public TweetViewModel Tweet { get; set; }
    }
}