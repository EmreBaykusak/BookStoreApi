using AutoMapper;
using BookStoreApi.Application.BookOperations.Commands.CreateBook;
using BookStoreApi.Application.BookOperations.Queries.GetBookDetail;
using BookStoreApi.Application.BookOperations.Queries.GetBooks;
using BookStoreApi.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreApi.Application.GenreOperations.Queries.GetGenres;

using BookStoreApi.Entities;

namespace BookStoreApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookCommand.CreateBookModel, Book>();
        CreateMap<Book, GetBookDetailQuery.BookDetailViewModel>()
            .ForMember(
                dest => dest.Genre,
                opt => opt.MapFrom
                    (src => src.Genre.Name));
        CreateMap<Book, GetBooksQuery.BookViewModel>().ForMember(
            dest => dest.Genre,
            opt => opt.MapFrom
                (src => src.Genre.Name));
        CreateMap<Genre, GetGenresQuery.GetGenresViewModel>(); 
        CreateMap<Genre, GetGenreDetailQuery.GetGenreDetailViewModel>();
    }
}