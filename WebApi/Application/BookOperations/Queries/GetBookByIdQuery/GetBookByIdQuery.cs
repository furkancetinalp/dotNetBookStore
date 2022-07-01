using WebApi.Common;
using WebApi.Entities;
using WebApi.DBOperations;
using AutoMapper;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBookByIdQuery
{
    public class GetBookByIdQuery
    {
        public int BookId {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public GetBookByIdModel Handle()
        {
            var book = _context.Books.Include(x=>x.Genre).Include(x=>x.Author).SingleOrDefault(x=>x.Id==BookId); //should be included (include(x=>x.Genre))
            if(book is null)
            {
                throw new InvalidOperationException("Book is not in the library!!!");
            }
            var result = _mapper.Map<GetBookByIdModel>(book);
            return result;
        }

    }
    public class GetBookByIdModel
    {
        public string Title {get;set;}
        public int PageCount {get;set;}
        public DateTime PublishDate {get;set;}
        public string Genre{get;set;}
        public string Author {get;set;}
    }
}