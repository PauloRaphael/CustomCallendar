using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataRepository.Data;
using DataRepository.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomCalendarMVC.Controllers
{
    public class BlocksController : Controller
    {
        private readonly CustomCalendarDBContext _context;

        public BlocksController(CustomCalendarDBContext context)
        {
            _context = context;
        }

        // GET: Blocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Block.OrderBy(b => b.Date).ToListAsync());
        }

        public async Task<IActionResult> Previous()
        {
            return View(await _context.Block.OrderBy(b => b.Date).ToListAsync());
        }
        // GET: Blocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _context.Block
                .Include(b => b.Category) // Include the Category
                .FirstOrDefaultAsync(m => m.Id == id);

            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }


        public IActionResult Create()
        {
            var categories = _context.Category.ToList(); // Ensure this is not null
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Block block)
        {
            if (ModelState.IsValid)
            {
                _context.Add(block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            foreach(var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            ModelState.Remove("Category");

            // Reassign ViewBag.CategoryId if ModelState is invalid
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", block.CategoryId);
            return View(block);
        }

        // GET: Blocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _context.Block.FindAsync(id);
            if (block == null)
            {
                return NotFound();
            }

            // Populate categories for the dropdown
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", block.CategoryId);

            return View(block);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,EventText,Important,CategoryId")] Block block)
        {
            if (id != block.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(block);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockExists(block.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag.CategoryId if validation fails
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", block.CategoryId);
            return View(block);
        }


        // GET: Blocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _context.Block
                .Include(b => b.Category) // Include the Category
                .FirstOrDefaultAsync(m => m.Id == id);

            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }


        // POST: Blocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var block = await _context.Block.FindAsync(id);
            if (block != null)
            {
                _context.Block.Remove(block);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockExists(int id)
        {
            return _context.Block.Any(e => e.Id == id);
        }
    }
}
