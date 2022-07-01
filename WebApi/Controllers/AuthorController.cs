
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using AutoMapper;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorByName;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController:ControllerBase
    {
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query= new GetAuthorsQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{name}")]
        public IActionResult GetAuthorById(string name)
        {
            GetAuthorByNameQuery query = new GetAuthorByNameQuery(_context,_mapper);
            query.AuthorName= name;
            GetAuthorByNameQueryValidator validator = new GetAuthorByNameQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);  
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model=newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }

        [HttpPut("{Fullname}")]
        public IActionResult UpdateAuthor(string Fullname, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.Fullname=Fullname;
            command.Model=updatedAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteAuthor(string name)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Name=name;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }

        
    }
}