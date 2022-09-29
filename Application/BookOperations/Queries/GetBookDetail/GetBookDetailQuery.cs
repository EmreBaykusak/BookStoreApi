using AutoMapper;
using BookStoreApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int BookId { get; set; }
    
    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == BookId);

        if (book is null)
        {
            throw new InvalidOperationException("This book doesn't exists");
        }
        
        BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
        return vm;
    }
    
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
    
    
}