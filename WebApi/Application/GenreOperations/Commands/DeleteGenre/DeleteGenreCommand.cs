using WebApi.DBOperations;
using System;
using System.Linq;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int ID {get;set;}
        private readonly bookstore3DbContext _context;

        public DeleteGenreCommand(bookstore3DbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Id==ID);
            if(genre is null)
            {
                throw new InvalidOperationException("Genre does not exist in the library!!!");
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }


    }
}