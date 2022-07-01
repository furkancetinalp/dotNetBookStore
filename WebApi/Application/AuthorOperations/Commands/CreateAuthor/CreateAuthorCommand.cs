
using System;
using AutoMapper;
using WebApi.DBOperations;
using System.Linq;
using WebApi.Entities;
namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorViewModel Model {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>(x.Name+x.Surname).ToLower()==(Model.Name+Model.Surname).ToLower());
            if(author is not null)
            {
                throw new InvalidOperationException("Author already exists in the library!!!");
            }
            author = new Author();
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();

        }

    }
    public class CreateAuthorViewModel
    {
        public string Name {get;set;}
        public string Surname {get;set;}
        public DateTime Birthdate {get;set;}
    }
}