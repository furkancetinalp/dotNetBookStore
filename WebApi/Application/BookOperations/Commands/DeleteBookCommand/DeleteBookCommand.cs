
using WebApi.DBOperations;
using AutoMapper;
using System.Linq;
using System;
namespace WebApi.Application.BookOperations.Commands.DeleteBookCommand
{
    public class DeleteBookCommand
    {
        public int BookId {get;set;}
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommand(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x=>x.Id==BookId);
            if(book is null)
            {
                throw new InvalidOperationException("Book is not in the library!!!!");
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}