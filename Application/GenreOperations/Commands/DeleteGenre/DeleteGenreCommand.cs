using BookStoreApi.DBOperations;
using Microsoft.Extensions.Logging.Abstractions;

namespace BookStoreApi.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly BookStoreDbContext _context;
    public int GenreId { get; set; }

    public DeleteGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("Genre can not be found");
        }

        _context.Genres.Remove(genre);
        _context.SaveChanges();
    }
}