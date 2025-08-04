using AutoMapper;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Domain.Entities;

namespace PublicLibraryService.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, MostBorrowedBook>()
                .ForMember(dest => dest.BorrowCount, opt => opt.Ignore());
            CreateMap<BookInventory, BookLending>();
        }
    }
}
