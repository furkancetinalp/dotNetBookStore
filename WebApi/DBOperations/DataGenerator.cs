
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Entities;
namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using ( var context = new bookstore3DbContext(serviceProvider.GetRequiredService<DbContextOptions<bookstore3DbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                

                context.Genres.AddRange(
                    new Genre
                    {
                        Name="Novel"
                    },
                    new Genre
                    {
                        Name="Science Fiction"
                    },
                    new Genre
                    {
                        Name="Personal Growth"
                    }

                );

                context.Books.AddRange(
                    new Book
                    {
                        Title="Faust",
                        GenreId =1,
                        PageCount=320,
                        PublishDate = new DateTime(1890,04,10),
                        AuthorId=1
                    },
                    new Book
                    {
                        Title="Hobbit",
                        GenreId =2,
                        PageCount=910,
                        PublishDate = new DateTime(1950,01,20),
                        AuthorId=2
                    },
                    new Book
                    {
                        Title="The Secret",
                        GenreId =3,
                        PageCount=160,
                        PublishDate = new DateTime(2000,07,04),
                        AuthorId=3
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name="Johann Wolfgang von",
                        Surname ="Goethe",
                        BirthDate= new DateTime(1749,08,28)
                    },
                    new Author
                    {
                        Name="John Ronald Reuel",
                        Surname="Tolken",
                        BirthDate=new DateTime(1892,01,3)

                    },
                    new Author
                    {
                        Name="Rhonda",
                        Surname="Byrne",
                        BirthDate=new DateTime(1951,03,12)
                    }
                    );
                context.SaveChanges();
            }

            
        }
    }
}