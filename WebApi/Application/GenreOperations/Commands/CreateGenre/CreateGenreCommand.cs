using WebApi.DBOperations;
using WebApi.Common;
using System;
using System.Linq;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model {get;set;}
        private readonly bookstore3DbContext _context;

        public CreateGenreCommand(bookstore3DbContext context)
        {
            _context=context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Name==Model.Name);
            if(genre is not null)
            {
                throw new InvalidOperationException("Genre is already in the library!!");
            }
            genre = new Genre();
            genre.Name=Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreViewModel
    {
        public string Name {get;set;}
    }
}