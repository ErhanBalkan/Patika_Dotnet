using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static CreateBookCommand;
using static GetBookDetailQuery;
using static UpdateBookCommand;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ConsoleLogger _consoleLogger;
    public BookController(BookStoreDbContext context, IMapper mapper, ConsoleLogger consoleLogger)
    {
        _context = context;
        _mapper = mapper;
        _consoleLogger = consoleLogger;
    }

    // private static List<Book> BookList = new List<Book>()
    // {
    //     new Book{
    //         Id = 1,
    //         Title = "Lean Startup",
    //         GenreId = 1, // Personal Growth
    //         PageCount = 200,
    //         PublishDate = new DateTime(2001,06,12)
    //     },
    //     new Book{
    //         Id = 2,
    //         Title = "Herland",
    //         GenreId = 2, 
    //         PageCount = 250,
    //         PublishDate = new DateTime(2010,05,23)
    //     },
    //     new Book{
    //         Id = 3,
    //         Title = "Dune",
    //         GenreId = 2, 
    //         PageCount = 540,
    //         PublishDate = new DateTime(2002,12,21)
    //     }
    // };

    [HttpGet]
    public IActionResult GetBooks(){
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        _consoleLogger.Write("Kitaplar listelendi.");
        return Ok(result);
    }

// -------------------------------------------------------------------------------------------

    [HttpGet("{id}")] // Route üzerinden parametreyi alacak.
    public IActionResult GetById(int id){
        BookDetailViewModel result;
        try{
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = id;
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();
        _consoleLogger.Write($"{id}'ye sahip kitap listelendi.");
        }catch(Exception ex){
            _consoleLogger.Write(ex.Message);
            return BadRequest(ex.Message);
        }

        Book book = _context.Books.Where(b => b.Id == id).SingleOrDefault(); // 1 tane getirmesi için SingleOrDefault()
        return Ok(result);
    }
    // Örnek route => http://localhost:5109/Books/3

// -------------------------------------------------------------------------------------------

    // Sadece 1 parametresiz HttpGet olabilir üsttekini yorum satırına almazsan hata verir.
    // [HttpGet] 
    // public Book GetById([FromQuery] string id){
    //     Book book = BookList.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault(); // 1 tane getirmesi için SingleOrDefault()
    //     return book;
    // }
    // Örnek query => http://localhost:5109/Books?id=3

// -------------------------------------------------------------------------------------------

    [HttpPost]
    public IActionResult AddBook([FromQuery] CreateBookModel newBook){
        CreateBookCommand command = new CreateBookCommand(_context,_mapper );
        try{
        command.Model = newBook;
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        // ValidationResult result = validator.Validate(command);
        // if(!result.IsValid){
        //     foreach (var item in result.Errors)
        //     {
        //         System.Console.WriteLine("Özellik: " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
        //     }
        // }else{
        //     command.Handle();
        // }
        validator.ValidateAndThrow(command); // direk hatayı gönderir üsttekilere gerek kalmaz.
        command.Handle();
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    // -------------------------------------------------------------------------------------------

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook){
        Book book = _context.Books.SingleOrDefault(b => b.Id == id);

        if (book is null)
            return BadRequest();
        
        // book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        // // eğer genreId default değer değilse updatedbooktakini verecek defaultsa aynı değeri verecek.
        // book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        // book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        // book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        try
        {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.Model = updatedBook;    
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        validator.ValidateAndThrow(command);        
        command.Handle();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id){
        try{
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = id;
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }
        return Ok();
    }


}