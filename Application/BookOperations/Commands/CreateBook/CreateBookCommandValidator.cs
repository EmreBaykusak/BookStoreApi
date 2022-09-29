using FluentValidation;

namespace BookStoreApi.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        RuleFor(command => command.Model.GenreId).LessThan(4);
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
    }
}