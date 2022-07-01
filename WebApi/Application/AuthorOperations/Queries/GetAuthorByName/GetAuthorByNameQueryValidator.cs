
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorByName
{
    public class GetAuthorByNameQueryValidator:AbstractValidator<GetAuthorByNameQuery>
    {
        public GetAuthorByNameQueryValidator()
        {
            RuleFor(query=>query.AuthorName).NotEmpty();
        }
        
        
    }
}