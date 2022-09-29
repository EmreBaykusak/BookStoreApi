using AutoMapper;
using BookStoreApi.Application.GenreOperations.Commands.CreateGenre;
using BookStoreApi.Application.GenreOperations.Commands.DeleteGenre;
using BookStoreApi.Application.GenreOperations.Commands.UpdateGenre;
using BookStoreApi.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreApi.Application.GenreOperations.Queries.GetGenres;
using BookStoreApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controller;

[ApiController]
[Route("[controller]s")]
public class GenreController: ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(IMapper mapper, BookStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new GetGenresQuery(_mapper, _context);
        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpGet("{id}")]
    public IActionResult GetDetailGenre(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper, _context);
        query.GenreId = id;
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var obj = query.Handle();

        return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreCommand.CreateGenreViewModel newGenre)
    {
        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = newGenre;
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);
            
        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre([FromBody] UpdateGenreCommand.UpdateGenreViewModel updatedGenre, int id)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.Model = updatedGenre;
        command.GenreId = id;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        
        validator.ValidateAndThrow(command);
        
        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = id;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        
        validator.ValidateAndThrow(command);
        
        command.Handle();

        return Ok();
    }
}