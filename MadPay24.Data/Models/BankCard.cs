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
        [StringLength(50)]
        public string BankName { get; set; }

        [Required]
        [StringLength(100)]
        public string OwnerName { get; set; }

        [StringLength(30, MinimumLength = 0)]
        public string Shaba { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 0)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(2,MinimumLength =0)]
        public string ExpireDateMonth { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 0)]
        public string ExpireDateYear { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
