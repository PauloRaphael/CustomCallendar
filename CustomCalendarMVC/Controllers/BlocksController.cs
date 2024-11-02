using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataRepository.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomCalendarMVC.Models;
using DataRepository.Services;

namespace CustomCalendarMVC.Controllers
{
    public class BlocksController : Controller
    {
        private readonly BlockService _blockService;
        private readonly CategoryService _categoryService;

        public BlocksController(BlockService blockService, CategoryService categoryService)
        {
            _blockService = blockService;
            _categoryService = categoryService;
        }

        // GET: Blocks
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService
                                                      .GetCategoriesAsync(), 
                                                      "Id", "Name");

            var blocks = await _blockService.GetFutureBlocksAsync();

            return View(new BlockViewModel { Block = blocks });
        }

        public async Task<IActionResult> PreviousAsync()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.
                                                      GetCategoriesAsync(), 
                                                      "Id", "Name");

            var blocks = await _blockService.GetPreviousBlocksAsync();
            return View(new BlockViewModel { Block = blocks });
        }

        // GET: Blocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _blockService.GetBlockAsync(id);

            if (block == null)
            {
                return NotFound();
            }

            return View(block);
        }


        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Block block)
        {
            if (ModelState.IsValid)
            {
                await _blockService.InsertAsync(block);
                return RedirectToAction(nameof(Index));
            }

            ModelState.Remove("Category");

            // Reassign ViewBag.CategoryId if ModelState is invalid
            ViewBag.CategoryId = new SelectList(await _categoryService
                                                      .GetCategoriesAsync(), 
                                                      "Id", "Name", 
                                                      block.CategoryId);
            return View(block);
        }

        public async Task<IActionResult> CreateMany()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMany(
            Block block, 
            int repetitions, 
            string span
            )
        {
            if (!ModelState.IsValid)
            {
                ModelState.Remove("Category");
                ViewBag.CategoryId = new SelectList(await _categoryService
                                                          .GetCategoriesAsync(), 
                                                          "Id", "Name", 
                                                          block.CategoryId);
                return View(block);
            }

            await _blockService.InsertMany(block, repetitions, span);
            return RedirectToAction(nameof(Index));
        }



        // GET: Blocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _blockService.GetBlockAsync(id);

            if (block == null)
            {
                return NotFound();
            }

            // Populate categories for the dropdown
            ViewBag.CategoryId = new SelectList(await _categoryService
                                                      .GetCategoriesAsync(), 
                                                      "Id", "Name", 
                                                      block.CategoryId);

            return View(block);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("Id,Title,Date,EventText,Important,CategoryId")] Block block
            )
        {
            if (id != block.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _blockService.UpdateBlockAsync(block);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_blockService.BlockExists(block.Id))
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
            ViewBag.CategoryId = new SelectList(await _categoryService
                                                      .GetCategoriesAsync(),
                                                      "Id", "Name",
                                                      block.CategoryId);
            return View(block);
        }


        // GET: Blocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var block = await _blockService.GetBlockAsync(id);

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
            var block = await _blockService.GetBlockAsync(id);

            if (block != null)
            {
                await _blockService.DeleteBlockAsync(block);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SearchAsync(
            int? CategoryId,
            DateTime? from,
            DateTime? to,
            bool important
            )
        {

            var blockList = await _blockService.SearchAsync(CategoryId, from, to, important);

            ViewBag.CategoryId = new SelectList(await _categoryService
                                                      .GetCategoriesAsync(),
                                                      "Id", "Name");

            return View(nameof(SearchAsync), new BlockViewModel { Block = blockList });

        }


        public async Task<IActionResult> DeleteOldBlocksAsync()
        {
            await _blockService.DeleteOldBlocks();

            return RedirectToAction(nameof(Index));
        }
    }
}
