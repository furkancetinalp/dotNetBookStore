using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Common;
using AutoMapper;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase
    {
        private readonly bookstore3DbContext _context;
        private readonly IMapper _mapper;
        

        public GenreController(bookstore3DbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var result= query.Handle();
            return Ok(result);
            
            
        }
        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            GetGenreDetailsQuery query= new GetGenreDetailsQuery(_context,_mapper);
            query.ID=id;
            GetGenreDetailsQueryValidator validator = new GetGenreDetailsQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model=newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreViewModel newGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.ID=id;
            command.Model=newGenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.ID=id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            
        }
    }
}