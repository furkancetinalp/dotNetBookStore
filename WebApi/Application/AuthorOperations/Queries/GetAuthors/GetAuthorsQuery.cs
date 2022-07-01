
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public List<GetAuthorsViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x=>x.Id).ToList<Author>();

            var result = _mapper.Map<List<GetAuthorsViewModel>>(authors);
            return result;

        }

    }

    public class GetAuthorsViewModel
    {
        public string name {get;set;}
        public string BirthDate {get;set;}
    }
}