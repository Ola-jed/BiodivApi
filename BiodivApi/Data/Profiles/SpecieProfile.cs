using AutoMapper;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;

namespace Biodiv.Data.Profiles
{
    public class SpecieProfile: Profile
    {
        public SpecieProfile()
        {
            CreateMap<SpecieCreateDto,Specie>();
            CreateMap<SpecieUpdateDto,Specie>();
            CreateMap<Specie,SpecieReadDto>();
            CreateMap<Specie,SpecieWithPhotosReadDto>();
        }
    }
}