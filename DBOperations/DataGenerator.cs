using BookStoreApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.DBOperations;

public class DataGenerator 
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>());
        if (context.Books.Any())
        {
            return;
        }
        
        context.Books.AddRange(
            new Book
            {
                Id = 1,
                Title = "Hunger Games",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001, 06, 12)
            },
        
            new Book
            {
                Id = 2,
                Title = "Harry Potter",
                GenreId = 1,
                PageCount = 500,
                PublishDate = new DateTime(2008, 02, 11)
            },
        
            new Book
            {
                Id = 3,
                Title = "War and Peace",
                GenreId = 2,
                PageCount = 600,
                PublishDate = new DateTime(2003, 05, 22)
            });

        if (context.Genres.Any())
        {
            return;
        }
        
        context.Genres.AddRange(
            new Genre()
            {
                Id = 1,
                Name = "Science Fiction"
            },
            new Genre()
            {
                Id = 2,
                Name = "Personal Growth"
            },
            new Genre()
            {
                Id = 3,
                Name = "Romance"
            });
        context.SaveChanges();
    }
}