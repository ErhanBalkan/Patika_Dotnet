public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle(){
        Book book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
        if (book is null)
            throw new InvalidOperationException("Silinecek kitap bulunamadı.");

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}