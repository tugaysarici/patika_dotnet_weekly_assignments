using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book{
                        //Id = 1,
                        Title = "Lean Startup",
                        AuthorId = 1,
                        GenreId = 1,
                        PageCount = 214,
                        PublishDate = new System.DateTime(2001,06,12)
                    },
                    new Book{
                        //Id = 2,
                        Title = "Herland",
                        AuthorId = 2,
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new System.DateTime(2010,05,23)
                    },
                    new Book{
                        //Id = 3,
                        Title = "Dune",
                        AuthorId = 3,
                        GenreId = 2,
                        PageCount = 540,
                        PublishDate = new System.DateTime(2002,12,21)
                    }
                );

                context.Authors.AddRange(
                    new Author{
                        Name = "Eric",
                        Surname = "Ries",
                        BirthDate = new System.DateTime(1978,9,22)
                    },
                    new Author{
                        Name = "Charlotte Perkins",
                        Surname = "Gilman",
                        BirthDate = new System.DateTime(1860,7,3)
                    },
                    new Author{
                        Name = "Frank",
                        Surname = "Herbert",
                        BirthDate = new System.DateTime(1920,10,8)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}