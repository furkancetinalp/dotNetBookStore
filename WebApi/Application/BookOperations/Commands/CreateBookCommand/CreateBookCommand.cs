using WebApi.DBOperations;
using System;
using WebApi.Common;
using AutoMapper;
using WebApi.Entities;
using System.Linq;
using WebApi.Application.AuthorOperations.Queries.GetAuthorByName;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Application.BookOperations.Commands.CreateBookCommand
{
    public class CreateBookCommand
    {
        public CreateBookViewModel Model {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public void Handle()
        {
            var book= _context.Books.Include(x=>x.Author).SingleOrDefault(x=>x.Title==Model.Title);
            if(book is not null)
            {
                throw new InvalidOperationException("Book is already in the library!");
            }
            book = new Book();
            var check = _context.Authors.SingleOrDefault(x=>(x.Name+x.Surname).ToLower()==(Model.AuthorName+Model.AuthorSurname).ToLower());
            if(check is null)
            {
                var newAuthor = new Author();
                newAuthor.Name=Model.AuthorName.Trim();
                newAuthor.Surname=Model.AuthorSurname.Trim();
                _context.Authors.Add(newAuthor);

                book=_mapper.Map<Book>(Model);
                book.AuthorId=newAuthor.Id;            
            }
            else
            {
                book=_mapper.Map<Book>(Model);
                book.AuthorId=check.Id;
            }
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
    public class CreateBookViewModel
    {
        public string Title {get;set;}
        public int GenreId {get;set;}

        public int PageCount {get;set;}
        public DateTime PublishDate {get;set;}
        public string AuthorName {get;set;}
        public string AuthorSurname {get;set;}
    }
}