using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var book = _context.Books.SingleOrDefault(x => x.AuthorId == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("Author not found.");
            }
            if(book is not null)
            {
                throw new InvalidOperationException("Author cannot be deleted because there is at least one book record belonging to this author.");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}