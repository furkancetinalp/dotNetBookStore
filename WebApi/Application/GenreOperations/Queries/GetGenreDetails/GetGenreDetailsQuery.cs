using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailsQuery
    {
        public int ID {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailsQuery(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public GetGenreDetailsViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.Id==ID);
            if(genre is null)
            {
                throw new InvalidOperationException("Genre is not in the library!!!");

            }
            var result = _mapper.Map<GetGenreDetailsViewModel>(genre);
            return result;

        }

        
    }
    public class GetGenreDetailsViewModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
    }
}