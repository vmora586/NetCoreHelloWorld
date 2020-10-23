using AutoMapper;
using BooksApi.Entities;
using Entities;

namespace BooksApi.Dtos
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<AuthorDto, Author>().ReverseMap();//to enable side by side mapping
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<CreateAuthorDto, AuthorDto>();
            CreateMap<BookDto,Book>();
            CreateMap<Book,BookDto>();
        }
    }
}