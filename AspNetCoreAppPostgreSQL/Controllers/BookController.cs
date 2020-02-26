using AspNetCoreAppPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Controllers
{
    public class BookController : Controller
    {
        private readonly TvShowsContext _context;

        public BookController(TvShowsContext context)
        {
            _context = context;
        }

        // GET: TvShows
        public async Task<IActionResult> BookIndex()
        {
            return View("BookIndex", await _context.Books.ToListAsync());
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Img,Name,Author,Year,Price,DownloadLink,Text")] CreateBookRequest request)
        {
            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(BookIndex));
            }

            return View("Error");
        }

        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(BookIndex));
        }
    }

    public class CreateBookRequest
    {
        public string Img { get; set; } //base64string
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public double Price { get; set; }
        public string DownloadLink { get; set; }
        public string Text { get; set; }
    }
}
