using System;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Model.GenreModel;
using BusinessLogic.Model.MovieModel;
using DataAccounts;
using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Movie, MovieInfo>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre.Name).ToList()));
    }
}