using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceSummarizer.Models
{
    public class StatementDetails
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date Time")]
        [DataType(DataType.Date)]
        public DateTime CreatedDateTime { get; set; }

        public int Bank { get; set; }

        public int Branch { get; set; }

        public string Account { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [Display(Name = "Available Balance")]
        public decimal AvailableBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Available Balance Date")]
        [DataType(DataType.Date)]
        public DateTime AvailableBalanceDate { get; set; }

        [Display(Name = "Ledger Balance")]
        public decimal LedgerBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ledger Balance Date")]
        [DataType(DataType.Date)]
        public DateTime LedgerBalanceDate { get; set; }
    }
}
