using AutoMapper;
using SmartSchool.Data.DTOs;
using SmartSchool.Data.Models;
using System.Globalization;

namespace SmartSchool.API.Helpers
{
    public class SmartSchollProfile : Profile
    {
        public SmartSchollProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                  dest => dest.Nome,
                  opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                 .ForMember(
                  dest => dest.Idade,
                  opt => opt.MapFrom(src => src.DataNascimento.GetCurranceAge())
                );                
        }
    }
}
