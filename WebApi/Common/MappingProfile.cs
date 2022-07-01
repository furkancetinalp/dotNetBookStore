using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBookCommand;
using WebApi.Entities;
using WebApi.Application.BookOperations.Queries.GetBooksQuery;
using WebApi.Application.BookOperations.Queries.GetBookByIdQuery;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorByName;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace WebApi.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookViewModel,Book>();
            
            CreateMap<Book,GetBooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest=>dest.Author,opt=>opt.MapFrom(src=>src.Author.Name+" "+src.Author.Surname));

            CreateMap<Book,GetBookByIdModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest=>dest.Author,opt=>opt.MapFrom(src=>src.Author.Name+" "+src.Author.Surname));

            CreateMap<Genre,GetGenresViewModel>();

            CreateMap<Genre,GetGenreDetailsViewModel>();



            //Authors
            CreateMap<Author,GetAuthorsViewModel>().ForMember(dest=>dest.name,opt=>opt.MapFrom(src=>src.Name+" "+src.Surname));

            CreateMap<Author,GetAuthorByNameViewModel>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name+" "+src.Surname));


            CreateMap<CreateAuthorViewModel,Author>();

        }
    }
}