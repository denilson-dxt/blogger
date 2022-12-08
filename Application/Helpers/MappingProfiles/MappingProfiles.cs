using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Tag, TagDto>();
        CreateMap<Post, PostDto>();
        CreateMap<Folder, FolderDto>();
        CreateMap<Domain.File, FileDto>();
    }
}