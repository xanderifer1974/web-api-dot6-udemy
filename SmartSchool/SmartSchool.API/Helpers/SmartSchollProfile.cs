using AutoMapper;
using SmartSchool.Data.DTOs;
using SmartSchool.Data.Models;

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

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRecordDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
               .ForMember(
                   dest => dest.Nome,
                   opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
               );

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRecordDto>().ReverseMap();
        }
    }
}
