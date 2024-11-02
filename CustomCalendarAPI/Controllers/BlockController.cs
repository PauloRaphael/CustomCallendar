using DataRepository.Data;
using DataRepository.Entities;
using DataRepository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CustomCalendarAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : Controller
    {
        private readonly BlockService _blockService;

        public BlockController(BlockService blockService)
        {
            _blockService = blockService;
        }

        //GET: api/Block
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Block>>> GetBlocks()
        {
            var blocks = _blockService.GetBlocksAsync();
            return Ok(blocks);
        }

        //GET api/Block/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Block>> GetBlockById(int id)
        {
            var block = await _blockService.GetBlockAsync(id);

            if (block == null)
            {
                return NotFound("Id not found");
            }

            return block;
        }

        [HttpPost]
        public async Task<ActionResult<Block>> PostBlock(Block block)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid block");
            }

            await _blockService.InsertAsync(block);

            return CreatedAtAction(nameof(GetBlockById), new { id = block.Id }, block);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Block>> PutBlock(int id, Block block)
        {
            if (id != block.Id)
            {
                return BadRequest();
            }

            try
            {
                await _blockService.UpdateBlockAsync(block);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_blockService.BlockExists(id))
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
            var aluno = await _blockService.GetBlockAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            await _blockService.DeleteBlockAsync(aluno);

            return NoContent();
        }
    }
}
