using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book { Title = "Lean Startup", GenreId = 1, PageCount = 214, PublishDate = new System.DateTime(2001,06,12) },
                new Book { Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new System.DateTime(2010,05,23) },
                new Book { Title = "Dune", GenreId = 2, PageCount = 540, PublishDate = new System.DateTime(2002,12,21) } 
            );

            
        }
    }
}