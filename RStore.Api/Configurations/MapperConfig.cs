using AutoMapper;
using RStore.Api.Data;
using RStore.Api.Dto.Author;
using RStore.Api.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RStore.Api.Configurations;

public class MapperConfig : Profile
{
	public MapperConfig()
	{
		CreateMap<AuthorCreateDto, Author>().ReverseMap();
        CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
        CreateMap<AuthorUpdateDto, Author>().ReverseMap();

        CreateMap<BookCreateDto, Book>().ReverseMap();
        CreateMap<BookUpdateDto, Book>().ReverseMap();
        CreateMap<Book, BookReadOnlyDto>()
            .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.Firstname} {map.Author.Lastname}"))
            .ReverseMap();
        CreateMap<Book, BookDetailsDto>()
            .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.Firstname} {map.Author.Lastname}"))
            .ReverseMap();
    }
}
