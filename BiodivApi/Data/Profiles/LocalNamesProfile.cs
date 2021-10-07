using AutoMapper;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;

namespace Biodiv.Data.Profiles
{
    public class LocalNamesProfile: Profile
    {
        public LocalNamesProfile()
        {
            CreateMap<LocalNameCreateDto,LocalName>();
            CreateMap<LocalNameUpdateDto,LocalName>();
        }
    }
}