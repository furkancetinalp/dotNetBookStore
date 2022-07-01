
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using System.Linq;
using WebApi.Entities;
using WebApi.Application.BookOperations.Commands.CreateBookCommand;
using WebApi.Application.BookOperations.Queries.GetBooksQuery;
using WebApi.Application.BookOperations.Queries.GetBookByIdQuery;
using WebApi.Application.BookOperations.Commands.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.DeleteBookCommand;
using AutoMapper;
using FluentValidation;
using System;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        
        public BookController(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
            
        }
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context,_mapper);
            query.BookId=id;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model=newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel newBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId=id;
            command.Model=newBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context,_mapper);
            command.BookId=id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            
        }
        

    }
}