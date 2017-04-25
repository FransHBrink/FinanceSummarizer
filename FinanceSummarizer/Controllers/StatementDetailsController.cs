using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceSummarizer.Models;

namespace FinanceSummarizer.Controllers
{
    public class StatementDetailsController : Controller
    {
        private readonly FinanceSummarizerContext _context;

        public StatementDetailsController(FinanceSummarizerContext context)
        {
            _context = context;    
        }

        // GET: StatementDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatementDetails.ToListAsync());
        }

        // GET: StatementDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statementDetails = await _context.StatementDetails
                .SingleOrDefaultAsync(m => m.ID == id);
            if (statementDetails == null)
            {
                return NotFound();
            }

            return View(statementDetails);
        }

        // GET: StatementDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatementDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreatedDateTime,Bank,Branch,Account,FromDate,ToDate,AvailableBalance,AvailableBalanceDate,LedgerBalance,LedgerBalanceDate")] StatementDetails statementDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statementDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statementDetails);
        }

        // GET: StatementDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statementDetails = await _context.StatementDetails.SingleOrDefaultAsync(m => m.ID == id);
            if (statementDetails == null)
            {
                return NotFound();
            }
            return View(statementDetails);
        }

        // POST: StatementDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedDateTime,Bank,Branch,Account,FromDate,ToDate,AvailableBalance,AvailableBalanceDate,LedgerBalance,LedgerBalanceDate")] StatementDetails statementDetails)
        {
            if (id != statementDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statementDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatementDetailsExists(statementDetails.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(statementDetails);
        }

        // GET: StatementDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statementDetails = await _context.StatementDetails
                .SingleOrDefaultAsync(m => m.ID == id);
            if (statementDetails == null)
            {
                return NotFound();
            }

            return View(statementDetails);
        }

        // POST: StatementDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statementDetails = await _context.StatementDetails.SingleOrDefaultAsync(m => m.ID == id);
            _context.StatementDetails.Remove(statementDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StatementDetailsExists(int id)
        {
            return _context.StatementDetails.Any(e => e.ID == id);
        }
    }
}
