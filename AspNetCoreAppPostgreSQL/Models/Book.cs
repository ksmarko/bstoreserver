using System.Collections.Generic;

namespace AspNetCoreAppPostgreSQL.Models
{
    public class Book
    {
        public Book()
        {
            Reviews = new List<Review>();
        }

        public int Id { get; set; }
        public string Img { get; set; } //base64string
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public double Price { get; set; }
        public List<Review> Reviews { get; set; }

        public string DownloadLink { get; set; }
        public string Text { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
