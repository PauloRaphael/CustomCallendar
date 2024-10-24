using DataRepository.Data;
using DataRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomCalendarAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : Controller
    {
        private readonly CustomCalendarDBContext _dbContext;

        public BlockController(CustomCalendarDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: api/Block
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Block>>> GetBlock()
        {
            List<Block> blocks = await _dbContext.Block.ToListAsync();
            return Ok(blocks);
        }

        //GET api/Block/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Block>> GetBlock(int id)
        {
            var block = await _dbContext.Block.FindAsync(id);

            if (block == null)
            {
                return NotFound("Id not found");
            }

            return block;
        }

        [HttpPost]
        public async Task<ActionResult<Block>> PostBlock(Block block)
        {
            _dbContext.Block.Add(block);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlock), new { id = block.Id }, block);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Block>> PutBlock(int id, Block block)
        {
            if (id != block.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(block).State = EntityState.Modified;


            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlockExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBlock(int id)
        {
            var aluno = await _dbContext.Block.FindAsync(id);
                                               
            if (aluno == null)
            {
                return NotFound();
            }

            _dbContext.Block.Remove(aluno);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool BlockExists(int id)
        {
            return _dbContext.Block.Any(e => e.Id == id);
        }

    }

    
}
