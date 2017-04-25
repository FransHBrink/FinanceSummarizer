using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceSummarizer.Models
{
    public class Transaction
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Unique Id")]
        public int UniqueId { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [Display(Name = "Cheque Number")]
        public string ChequeNumber { get; set; }

        public string Payee { get; set; }

        public string Memo { get; set; }

        public decimal Amount { get; set; }
    }
}
