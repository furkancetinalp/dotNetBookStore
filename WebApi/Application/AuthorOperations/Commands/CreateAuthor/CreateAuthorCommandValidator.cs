using FluentValidation;
using System;
namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator: AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty();
            RuleFor(Command=>Command.Model.Surname).NotEmpty();
            RuleFor(command=>command.Model.Birthdate).LessThan(DateTime.Now.Date);
        }
    }
}