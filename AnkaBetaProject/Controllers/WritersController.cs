using AnkaBetaProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnkaBetaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WritersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public WritersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/writers
        [HttpGet]
        public async Task<List<WriterViewModel>> GetWriters()
        {
            using (var context = _dbContext)
            {
                var writers = await context.Writers.Select(w => new WriterViewModel
                {
                    WriterId = w.Id,
                    Name = w.Name
                })
                    .ToListAsync();

                return writers;
            }
        }

        // GET: api/writers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WriterViewModel>> GetWriter(int id)
        {
            var writer = await _dbContext.Writers.Select(w => new WriterViewModel
            {
                WriterId= w.Id,
                Name = w.Name
            }).ToListAsync();

            if (writer.Find(x => x.WriterId == id) == null)
            {
                return NotFound();
            }

            return writer.Where(x => x.WriterId == id).First();
        }

        // POST: api/writers
        [HttpPost]
        public async Task<ActionResult<Writer>> CreateWriter(WriterCreateModel writer)
        {
            Writer w = new Writer();
            w.Name = writer.Name;

            _dbContext.Writers.Add(w);
            await _dbContext.SaveChangesAsync();

           var _writer = await _dbContext.Writers.Select(_w => new WriterViewModel
           {
               WriterId=_w.Id,
               Name= _w.Name
           }).Where(_w => _w.WriterId == w.Id).FirstAsync();

            WriterViewModel writerViewModel = new WriterViewModel();
            writerViewModel.WriterId = _writer.WriterId;
            writerViewModel.Name = writer.Name;

            return CreatedAtAction(nameof(GetWriter), new { id = w.Id }, writerViewModel);
        }

        // PUT: api/writers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWriter(int id,WriterUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var writer = await _dbContext.Writers.FindAsync(id);
            if (writer == null)
            {
                return NotFound();
            }

            writer.Name = model.Name;
            _dbContext.Entry(writer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
   
            return NoContent();
        }

        // DELETE: api/writers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWriter(int id)
        {
            var writer = await _dbContext.Writers.FindAsync(id);

            if (writer == null)
            {
                return NotFound();
            }

            _dbContext.Writers.Remove(writer);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
