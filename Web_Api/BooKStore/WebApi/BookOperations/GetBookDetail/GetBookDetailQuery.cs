using AutoMapper;

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

    public BookDetailViewModel Handle(){
        var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
        if (book is null)
        {
            throw new InvalidOperationException("Kitap bulunamadı.");
        }
        BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
        // vm.Title = book.Title;
        // vm.PageCount = book.PageCount;
        // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        return vm;
    }

    public override bool Equals(object obj)
    {
        return obj is GetBookDetailQuery query &&
               EqualityComparer<IMapper>.Default.Equals(_mapper, query._mapper);
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}