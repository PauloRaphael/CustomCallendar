using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataRepository.Data;
using DataRepository.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomCalendarMVC.Models;

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
        public IActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name");
            var blocks = _context.Block.ToList();
            return View(new BlockViewModel { Block = blocks });
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

            ModelState.Remove("Category");

            // Reassign ViewBag.CategoryId if ModelState is invalid
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", block.CategoryId);
            return View(block);
        }

        public IActionResult CreateMany()
        {
            var categories = _context.Category.ToList(); // Ensure this is not null
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMany(Block block, int repetitions, string span)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Remove("Category");
                ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", block.CategoryId);
                return View(block);
            }

            var blocksToAdd = new List<Block>(); 
            var initialDate = block.Date; 

            for (int i = 0; i < repetitions; i++)
            {

                var newBlock = new Block
                {
                    Id = 0,
                    Title = block.Title,
                    EventText = block.EventText,
                    Important = block.Important,
                    CategoryId = block.CategoryId
                };

                switch (span)
                {
                    case "Yearly":
                        newBlock.Date = initialDate.AddYears(i);
                        break;
                    case "Monthly":
                        newBlock.Date = initialDate.AddMonths(i);
                        break;
                    case "Daily":
                        newBlock.Date = initialDate.AddDays(i);
                        break;
                }

                blocksToAdd.Add(newBlock);
            }

            _context.Block.AddRange(blocksToAdd);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        public IActionResult Search(int? CategoryId, DateTime? from, DateTime? to)
        {
            // Start with all blocks
            var blocks = _context.Block.AsQueryable();

            if (CategoryId.HasValue)
            {
                blocks = blocks.Where(b => b.CategoryId == CategoryId.Value);
            }

            if (from.HasValue)
            {
                blocks = blocks.Where(b => b.Date >= from.Value);
            }

            if (to.HasValue)
            {
                blocks = blocks.Where(b => b.Date <= to.Value);
            }

            var blockList = blocks.ToList();

            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name");

            return View(nameof(Index), new BlockViewModel { Block = blockList });
        }


        public IActionResult DeleteOldBlocks()
        {
            var currentTime = DateTime.Now;
            var oldBlocks = _context.Block.Where(b => b.Date < currentTime).ToList();

            if (oldBlocks.Count != 0)
            {
                _context.Block.RemoveRange(oldBlocks);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
