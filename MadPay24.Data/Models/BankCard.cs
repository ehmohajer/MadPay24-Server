using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MadPay24.Data.Models
{
    public class BankCard:BaseEntity<string>
    {
        public BankCard()
        {
            Id = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
        [Required]
        public string BankName { get; set; }
        public string Shaba { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(2,MinimumLength =2)]
        public string ExpireDateMonth { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireDateYear { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
