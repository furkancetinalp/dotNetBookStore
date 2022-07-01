
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.Commands.DeleteBookCommand;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public string Name {get;set;}
        private readonly bookstore3DbContext _context;

        public DeleteAuthorCommand(bookstore3DbContext context)
        {
            _context=context;
        }
        public void Handle()
        {
            _context.SaveChanges();
            var author = _context.Authors.Include(x=>x.Book).Where(x=>(x.Name+" "+x.Surname).ToLower()==Name.ToLower()).FirstOrDefault();
            if(author is null)
            {
                throw new InvalidOperationException("Author does not exist in the library!!!");
            }
            var check = _context.Books.Where(x=>x.AuthorId==author.Id).SingleOrDefault();
            if(check is not null)
            {
                throw new InvalidOperationException("Author cannot be removed because the author has at least 1 book which is registered in library!!!");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}