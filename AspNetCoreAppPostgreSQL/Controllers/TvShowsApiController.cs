using AspNetCoreAppPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Controllers
{
    [ApiController]
    [Route("/api/tvshows")]
    public class TvShowsApiController : ControllerBase
    {
        private readonly TvShowsContext _context;

        public TvShowsApiController(TvShowsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TvShow>>> Index()
        {
            return await _context.TvShow.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TvShow>> Details(int id)
        {
            var tvShow = await _context.TvShow.FirstOrDefaultAsync(m => m.Id == id);

            if (tvShow == null)
            {
                return NotFound();
            }

            return tvShow;
        }
    }
}
