using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
        CreateMap<GenreDto, Genre>();
    }
}