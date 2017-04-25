using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceSummarizer.Models
{
    public class Statement
    {
        public StatementDetails StatementDetails { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
