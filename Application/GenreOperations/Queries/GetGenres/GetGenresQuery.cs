using AutoMapper;
using BookStoreApi.DBOperations;

namespace BookStoreApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    private readonly IMapper _mapper;
    private readonly BookStoreDbContext _context;

    public GetGenresQuery(IMapper mapper, BookStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public List<GetGenresViewModel> Handle()
    {
        var genres = _context.Genres.OrderBy(x=> x.Id).Where(x => x.IsActive == true).ToList();
        var returnObj = _mapper.Map<List<GetGenresViewModel>>(genres);
        return returnObj;
    }

    public class GetGenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}