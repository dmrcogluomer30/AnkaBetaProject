using AnkaBetaProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnkaBetaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BooksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/books
        [HttpGet]
        public async Task<List<BookViewModel>> GetBooks()
        {
            using (var context = _dbContext)
            {
                var books = await context.Books
                    .Select(b => new BookViewModel
                    {
                        BookId = b.Id,
                        Title = b.Title,
                        WriterId = b.Writer.Id,
                        WriterName = b.Writer.Name,
                        LibraryId = b.LibraryId,
                        LibraryName = b.Library.Name
                    })
                    .ToListAsync();

                return books;
            }
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetBook(int id)
        {
            var book = await _dbContext.Books.Select(b => new BookViewModel
            {
                BookId = b.Id,
                Title = b.Title,
                WriterId = b.Writer.Id,
                WriterName = b.Writer.Name,
                LibraryId = b.LibraryId,
                LibraryName = b.Library.Name
            }).ToListAsync();

            if (book.Find(x=> x.BookId == id) == null)
            {
                return NotFound();
            }

            return book.Where(x => x.BookId == id).First();
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookCreateModel book)
        {
            Book b = new Book();
            b.Title = book.Title;
            b.WriterId = book.WriterId;
            b.LibraryId = book.LibraryId;

            _dbContext.Books.Add(b);
            await _dbContext.SaveChangesAsync();


            var _book = await _dbContext.Books.Select(_b => new BookViewModel
            {
                BookId = _b.Id,
                Title = _b.Title,
                WriterId = _b.Writer.Id,
                WriterName = _b.Writer.Name,
                LibraryId = _b.LibraryId,
                LibraryName = _b.Library.Name
            }).Where(_b => _b.BookId == b.Id).FirstAsync();

            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.BookId = _book.BookId;
            bookViewModel.Title = _book.Title;
            bookViewModel.WriterId = _book.WriterId;
            bookViewModel.WriterName = _book.WriterName;
            bookViewModel.LibraryId = _book.LibraryId;
            bookViewModel.LibraryName = _book.LibraryName;
            

            return CreatedAtAction(nameof(GetBook), new { id = b.Id }, bookViewModel);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookUpdateModel model)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

          
            book.Title = model.Title;
            if(model.WriterId != null )book.WriterId = (int)model.WriterId;
            if (model.LibraryId != null) book.LibraryId = (int)model.LibraryId;

            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
