
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class bookstore3DbContext :DbContext
    {
        public bookstore3DbContext(DbContextOptions<bookstore3DbContext> options): base(options)
        {

        }
        public DbSet<Book> Books {get;set;}
        public DbSet<Genre> Genres {get;set;}

        public DbSet<Author> Authors {get;set;}

    }
}