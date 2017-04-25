using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceSummarizer.Models
{
    public class FinanceSummarizerContext : DbContext
    {
        public FinanceSummarizerContext (DbContextOptions<FinanceSummarizerContext> options)
            : base(options)
        {
        }

        public DbSet<FinanceSummarizer.Models.StatementDetails> StatementDetails { get; set; }
    }
}
