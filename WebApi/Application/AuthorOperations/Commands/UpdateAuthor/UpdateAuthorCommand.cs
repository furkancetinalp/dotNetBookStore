
using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public string Fullname {get;set;}
        public UpdateAuthorViewModel Model {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommand(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public void Handle()
        {
            var author = _context.Authors.Where(x=>(x.Name+" " +x.Surname).ToLower()==(Fullname).ToLower()).SingleOrDefault();
            if(author is null)
            {
                throw new InvalidOperationException("Invalid Author name selection!!!");
            }
            author.BirthDate=Model.BirthDate!=default? Model.BirthDate : author.BirthDate;
            _context.SaveChanges();
        }


    }
    public class UpdateAuthorViewModel
    {
        public DateTime BirthDate {get;set;}
    }
}