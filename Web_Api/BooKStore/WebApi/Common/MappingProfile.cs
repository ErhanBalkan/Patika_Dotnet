using AutoMapper;
using static CreateBookCommand;
using static GetBookDetailQuery;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.GenreId)).ToString();
    }
}