using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Common;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{

    public class GetGenresQuery
    {
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public List<GetGenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x=>x.IsActive).OrderBy(x=>x.Id).ToList<Genre>();
            var result = _mapper.Map<List<GetGenresViewModel>>(genres);
            return result;
        }
    }

    public class GetGenresViewModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
    }
}