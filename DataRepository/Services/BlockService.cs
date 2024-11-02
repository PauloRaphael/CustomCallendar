using DataRepository.Data;
using DataRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Services
{
    public class BlockService
    {
        private readonly CustomCalendarDBContext _context;

        public BlockService(CustomCalendarDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Block>> GetBlocksAsync()
        {
            return await _context.Block.ToListAsync();
        }

        public async Task<IEnumerable<Block>> GetFutureBlocksAsync()
        {
            return await _context.Block.Where(m => m.Date >= DateTime.Now).OrderBy(b => b.Date).ToListAsync();
        }
        public async Task<IEnumerable<Block>> GetPreviousBlocks()
        {
            return await _context.Block.Where(m => m.Date < DateTime.Now).OrderBy(b => b.Date).ToListAsync();
        }

        public async Task<Block?> GetBlockAsync(int id)
        {
            return await _context.Block.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task InsertAsync(Block block)
        {
            _context.Add(block);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlockAsync(Block block)
        {
            _context.Update(block);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlockAsync(Block block)
        {
            _context.Block.Remove(block);
            await _context.SaveChangesAsync();
        }
        public bool BlockExists(int id)
        {
            return _context.Block.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<Block>> Search(int? CategoryId, DateTime? from, DateTime? to, bool important)
        {

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

            if (important)
            {
                blocks = blocks.Where(b => b.Important == true);
            }

            return await blocks.ToListAsync();

        }

        public async Task DeleteOldBlocks()
        {
            var currentTime = DateTime.Now;
            var oldBlocks = _context.Block.Where(b => b.Date < currentTime).ToList();

            if (oldBlocks.Count != 0)
            {
                _context.Block.RemoveRange(oldBlocks);
                await _context.SaveChangesAsync();
            }
        }
    }
}
