using AutoMapper;
using RStore.Api.Data;
using RStore.Api.Dto.Author;
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

    }
}
