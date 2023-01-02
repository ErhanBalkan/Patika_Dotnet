using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>()
    {
        new Book{
            Id = 1,
            Title = "Lean Startup",
            GenreId = 1, // Personal Growth
            PageCount = 200,
            PublishDate = new DateTime(2001,06,12)
        },
        new Book{
            Id = 2,
            Title = "Herland",
            GenreId = 2, 
            PageCount = 250,
            PublishDate = new DateTime(2010,05,23)
        },
        new Book{
            Id = 3,
            Title = "Dune",
            GenreId = 2, 
            PageCount = 540,
            PublishDate = new DateTime(2002,12,21)
        }
    };

    [HttpGet]
    public List<Book> GetBooks(){
        List<Book> bookList = BookList.OrderBy(b => b.Id).ToList<Book>();
        return bookList;
    }

// -------------------------------------------------------------------------------------------

    [HttpGet("{id}")] // Route üzerinden parametreyi alacak.
    public Book GetById(int id){
        Book book = BookList.Where(b => b.Id == id).SingleOrDefault(); // 1 tane getirmesi için SingleOrDefault()
        return book;
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
    public IActionResult AddBook([FromQuery] Book newBook){
        Book book = BookList.SingleOrDefault(b => b.Title == newBook.Title);
        if (book is not null)
            return BadRequest();
        
        BookList.Add(newBook);
        return Ok();
    }

    // -------------------------------------------------------------------------------------------

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
        Book book = BookList.SingleOrDefault(b => b.Id == id);

        if (book is null)
            return BadRequest();
        
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        // eğer genreId default değer değilse updatedbooktakini verecek defaultsa aynı değeri verecek.
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        return Ok();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id){
        Book book = BookList.SingleOrDefault(b => b.Id == id);
        if (book is null)
            return BadRequest();

        BookList.Remove(book);
        return Ok();
    }


}