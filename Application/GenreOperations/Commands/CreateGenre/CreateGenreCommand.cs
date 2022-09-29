using BookStoreApi.DBOperations;
using BookStoreApi.Entities;

namespace BookStoreApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    private readonly BookStoreDbContext _context;
    public CreateGenreViewModel Model { get; set; }

    public CreateGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if (genre is not null)
        {
            throw new InvalidOperationException("This genre already exists");
        }

        genre = new Genre();
        genre.Name = Model.Name;
        _context.Genres.Add(genre);
        _context.SaveChanges();

    }

    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}