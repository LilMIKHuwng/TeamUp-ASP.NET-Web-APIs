using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.Contract.Repositories.Entity
{
    public class PaymentTemp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OrderCode { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } // "Court", "Coach", "Package"
        public int BookingId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
