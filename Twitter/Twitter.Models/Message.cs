using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Message
    {
        public int Id { get; set; }

        [MaxLength(200), MinLength(1)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        // [Required]
        public string RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public virtual User Recipient { get; set; }

        // [Required]
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }
    }
}
