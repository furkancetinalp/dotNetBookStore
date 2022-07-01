using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.AuthorOperations.Queries.GetAuthorByName;

namespace WebApi.Application.BookOperations.Commands.UpdateBookCommand
{
    public class UpdateBookCommand
    {
        public int BookId {get;set;}
        public UpdateBookModel Model {get;set;}
        private readonly bookstore3DbContext _context;

        public UpdateBookCommand(bookstore3DbContext context)
        {
            _context=context;
        }
        public void Handle()
        {
            var book = _context.Books.Include(x=>x.Author).SingleOrDefault(x=>x.Id==BookId);
            if(book is null)
            {
                throw new InvalidOperationException("Book cannot be found in the library");
            }
            book.Title=Model.Title!=default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId !=default ? Model.GenreId : book.GenreId;

            var check = _context.Authors.SingleOrDefault(x=>(x.Name+x.Surname).ToLower()==(Model.AuthorName+Model.AuthorSurname).ToLower());
            if(check is null)
            {
                var newAuthor = new Author();
                newAuthor.Name=Model.AuthorName.Trim();
                newAuthor.Surname=Model.AuthorSurname.Trim();
                _context.Authors.Add(newAuthor);
                book.AuthorId=newAuthor.Id;
            }
            else
            {
                book.AuthorId=check.Id;
            }
           
            _context.SaveChanges();
        }


    }
    public class UpdateBookModel
    {
        public string Title{get;set;}
        public int GenreId {get;set;}
        public string AuthorName {get;set;}
        public string AuthorSurname {get;set;}
    }

}