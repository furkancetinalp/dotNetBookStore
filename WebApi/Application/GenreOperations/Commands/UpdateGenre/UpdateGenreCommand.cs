using WebApi.DBOperations;
using System;
using System.Linq;


namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreViewModel Model {get;set;}
        public int ID {get;set;}
        private readonly bookstore3DbContext _context;

        public UpdateGenreCommand(bookstore3DbContext context)
        {
            _context=context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Id==ID );
            if(genre is null)
            {
                throw new InvalidOperationException("Genre does not exist in the library!!!");
            }
            if(_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower()&& x.Id!=ID))
            {
                throw new InvalidOperationException("Genre already exists in the library!!!!");
            }
            genre.Name =string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive=Model.IsActive;
            _context.SaveChanges();


        }

    }

    public class UpdateGenreViewModel
    {
        public string Name {get;set;}
        public bool IsActive {get;set;}

    }

}