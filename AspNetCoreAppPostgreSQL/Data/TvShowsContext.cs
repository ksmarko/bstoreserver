using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAppPostgreSQL.Models
{
    public class TvShowsContext : DbContext
    {
        public TvShowsContext (DbContextOptions<TvShowsContext> options)
            : base(options)
        {
        }

        public DbSet<TvShow> TvShow { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
