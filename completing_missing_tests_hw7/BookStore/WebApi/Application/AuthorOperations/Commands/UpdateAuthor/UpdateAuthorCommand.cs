using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException("Author not found.");
            }

            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() 
                                    && x.Surname.ToLower() == Model.Surname.ToLower()
                                    && x.BirthDate == Model.BirthDate && x.Id != AuthorId))
            {
                throw new InvalidOperationException("Same author already exists.");
            }
            
            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
            author.BirthDate = Model.BirthDate == DateTime.MinValue ? author.BirthDate : Model.BirthDate;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}