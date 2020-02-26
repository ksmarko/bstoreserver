using AspNetCoreAppPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Controllers
{
    [ApiController]
    [Route("/api/book")]
    public class BookApiController : ControllerBase
    {
        private readonly TvShowsContext _context;

        public BookApiController(TvShowsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _context.Books.Include(x => x.Reviews).FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddReview(int id, AddReviewRequest request)
        {
            var review = new Review
            {
                UserId = request.UserId,
                BookId = id,
                Text = request.Text
            };

            _context.Add(review);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(CreateBookRequest request)
        {
            var book = new Book
            {
                Img = request.Img,
                Name = request.Name,
                Author = request.Author,
                Year = request.Year,
                Price = request.Price,
                DownloadLink = request.DownloadLink,
                Text = request.Text
            };

            _context.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookRequest request)
        {
            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book != null)
            {
                book.Img = request.Img ?? book.Img;
                book.Name = request.Name ?? book.Name;
                book.Author = request.Author ?? book.Author;
                book.Year = request.Year ?? book.Year;
                book.Price = request.Price ?? book.Price;
                book.DownloadLink = request.DownloadLink ?? book.DownloadLink;
                book.Text = request.Text ?? book.Text;

                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }

    public class UpdateBookRequest
    {
        public string Img { get; set; } //base64string
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public double? Price { get; set; }
        public string DownloadLink { get; set; }
        public string Text { get; set; }
    }

    public class AddReviewRequest
    {
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
