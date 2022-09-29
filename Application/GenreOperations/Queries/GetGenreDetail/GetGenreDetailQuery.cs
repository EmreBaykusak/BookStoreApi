using AutoMapper;
using BookStoreApi.DBOperations;

namespace BookStoreApi.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    private readonly IMapper _mapper;
    private readonly BookStoreDbContext _context;
    public int GenreId { get; set; }
    public GetGenreDetailQuery(IMapper mapper, BookStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public GetGenreDetailViewModel Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.IsActive == true && x.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("Book genre doesn't exists");
        }
        var returnObj = _mapper.Map<GetGenreDetailViewModel>(genre);
        return returnObj;
    }

    public class GetGenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}