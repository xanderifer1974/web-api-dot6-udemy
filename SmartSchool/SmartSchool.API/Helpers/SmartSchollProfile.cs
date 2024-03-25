using AutoMapper;
using SmartSchool.Data.DTOs;
using SmartSchool.Data.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchollProfile: Profile
    {
        public SmartSchollProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                  dest => dest.Nome,
                  opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );
        }
    }
}
