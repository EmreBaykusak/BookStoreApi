using BookStoreApi.DBOperations;

namespace BookStoreApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly BookStoreDbContext _context;
    public int GenreId { get; set; }
    public UpdateGenreViewModel Model { get; set; }

    public UpdateGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("Book can not be found");
        }

        if (_context.Genres.Any(x=> string.Equals(x.Name, Model.Name, StringComparison.CurrentCultureIgnoreCase) && x.Id != GenreId ))
        {
            throw new InvalidOperationException("Book with the same name already exists");
        }

        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;
        _context.SaveChanges();
    }

    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}