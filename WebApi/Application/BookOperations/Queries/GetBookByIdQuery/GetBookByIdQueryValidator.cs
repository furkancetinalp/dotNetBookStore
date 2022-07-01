using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookByIdQuery
{
    public class GetBookByIdQueryValidator: AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}