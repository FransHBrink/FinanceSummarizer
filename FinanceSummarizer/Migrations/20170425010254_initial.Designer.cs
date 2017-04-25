using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FinanceSummarizer.Models;

namespace FinanceSummarizer.Migrations
{
    [DbContext(typeof(FinanceSummarizerContext))]
    [Migration("20170425010254_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinanceSummarizer.Models.StatementDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account");

                    b.Property<decimal>("AvailableBalance");

                    b.Property<DateTime>("AvailableBalanceDate");

                    b.Property<int>("Bank");

                    b.Property<int>("Branch");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<DateTime>("FromDate");

                    b.Property<decimal>("LedgerBalance");

                    b.Property<DateTime>("LedgerBalanceDate");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("ID");

                    b.ToTable("StatementDetails");
                });
        }
    }
}
