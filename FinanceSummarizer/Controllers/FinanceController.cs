using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Encodings.Web;
using FinanceSummarizer.Models;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceSummarizer.Controllers
{
    public class FinanceController : Controller
    {
        private IHostingEnvironment _environment;

        public FinanceController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // 
        // GET: /Finance/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            var fileUploadedPath = Path.Combine(uploads, file.FileName);

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(fileUploadedPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("Welcome");
            //// redirect back to the index action to show the form once again
        }

        // 
        // GET: /Finance/Welcome/ 
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            var statement = new Statement();
            var statementDetails = new StatementDetails();
            var transactionDetailsList = new List<Transaction>();

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            var fileUploadedPath = Path.Combine(uploads, "Export20170424235704.csv");

            using (var streamReader = System.IO.File.OpenText(fileUploadedPath))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();

                    if (line.Contains("Created date"))
                    {
                        statementDetails.CreatedDateTime = ExtractDateTime(line);
                    }
                    else if (line.Contains("Bank") && line.Contains("Branch"))
                    {
                        var bankBranchDetails = line.Split(';');
                        statementDetails.Bank = int.Parse(Regex.Replace(bankBranchDetails[0], @"\D", ""));
                        statementDetails.Branch = int.Parse(Regex.Replace(bankBranchDetails[1], @"\D", ""));
                        statementDetails.Account = Regex.Replace(bankBranchDetails[2], "Account", "");
                    }
                    else if (line.Contains("From date"))
                    {
                        var date = Regex.Replace(line, @"\D", "");
                        statementDetails.FromDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    else if (line.Contains("To date"))
                    {
                        var date = Regex.Replace(line, @"\D", "");
                        statementDetails.ToDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    else if (line.Contains("Avail Bal"))
                    {
                        var balance = CalculateAvailAndLedgBal(line);
                        statementDetails.AvailableBalance = Convert.ToDecimal(balance.FirstOrDefault().Replace(".", ","));
                        var date = balance.LastOrDefault();
                        statementDetails.AvailableBalanceDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    else if (line.Contains("Ledger Balance"))
                    {
                        var balance = CalculateAvailAndLedgBal(line);
                        statementDetails.LedgerBalance = Convert.ToDecimal(balance.FirstOrDefault().Replace(".", ","));
                        var date = balance.LastOrDefault();
                        statementDetails.LedgerBalanceDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        var data = line.Split(new[] { ',' });
                        if (DateTime.TryParse(data[0], out DateTime result))
                        {
                            transactionDetailsList.Add(new Transaction()
                            {
                                Date = result,
                                UniqueId = int.Parse(data[1]),
                                TransactionType = data[2],
                                ChequeNumber = data[3],
                                Payee = data[4],
                                Memo = data[5],
                                Amount = Convert.ToDecimal(data[6].Replace(".", ","))
                            });
                        }
                    }
                }
                statement.StatementDetails = statementDetails;
                statement.Transactions = transactionDetailsList;
            }
            DirectoryInfo di = new DirectoryInfo(uploads);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            return View(statement);
        }

        private IEnumerable<string> CalculateAvailAndLedgBal(string line)
        {
            var balanceAndDate = line.Split(':').LastOrDefault();
            return Regex.Split(balanceAndDate, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "");
        }

        private DateTime ExtractDateTime(string line)
        {
            var dateTimeString = line.Split('/');
            var date = dateTimeString[1].Split(':').LastOrDefault();
            var time = dateTimeString[2];
            return Convert.ToDateTime(date + time);
        }
    }
}
