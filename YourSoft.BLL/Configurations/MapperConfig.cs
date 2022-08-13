using AutoMapper;
using YourSoft.BLL.Models.Sample;
using YourSoft.BLL.Models.User;
using YourSoft.DAL.Data;

namespace YourSoft.BLL.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Sample, CreateSampleDto>().ReverseMap();
            CreateMap<Sample, SampleDto>().ReverseMap();

            CreateMap<ApiUserDto, ApiUser>().ReverseMap();
        }
    }
}
