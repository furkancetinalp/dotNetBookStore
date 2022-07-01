using System.Linq;
using System;
using WebApi.DBOperations;
using WebApi.Entities;
using AutoMapper;
namespace WebApi.Application.AuthorOperations.Queries.GetAuthorByName
{
    public class GetAuthorByNameQuery
    {
        public string AuthorName {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorByNameQuery(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public GetAuthorByNameViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>(x.Name+" "+x.Surname).ToLower() == AuthorName.ToLower());
            if(author is null)
            {
                throw new InvalidOperationException("Author does not exist in the library!!!");
            }
            var result = _mapper.Map<GetAuthorByNameViewModel>(author);
            return result;

        }
    }

    public class GetAuthorByNameViewModel
    {
        public string Name {get;set;}
        public string BirthDate {get;set;}
    }




}