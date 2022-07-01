using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command=>command.ID).GreaterThan(0);
            RuleFor(command=>command.Model.Name).MinimumLength(1).When(x=>x.Model.Name!=string.Empty);
        }
    }
}