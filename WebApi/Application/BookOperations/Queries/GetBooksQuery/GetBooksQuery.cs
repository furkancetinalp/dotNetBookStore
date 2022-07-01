using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using System;
using System.Linq;
using WebApi.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBooksQuery
{
    public class GetBooksQuery
    {
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public List<GetBooksViewModel> Handle()
        {
            var books = _context.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x=>x.Id).ToList<Book>(); //should be included
            var result = _mapper.Map<List<GetBooksViewModel>>(books);
            return result;
        }   
    }

    public class GetBooksViewModel
    {
        public string Title {get;set;}
        public int PageCount {get;set;}
        public DateTime PublishDate {get;set;}
        public string Genre {get;set;}
        public string Author {get;set;}
    }

}