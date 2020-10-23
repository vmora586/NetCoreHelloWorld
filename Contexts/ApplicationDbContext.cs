using BooksApi.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {
            
        }
        public DbSet<Author>Authors {get;set;}
        public DbSet<Book> Books { get; set; }
    }
}