using AutoMapper;

namespace ProjectMaCaisseAPI_V01.Profiles
{
    public class CustomProfile: Profile
    {

        //Mapper.CreateMap<Base, DTO>()
        //    .Include<ClassA, DTO_A>()
        //    .Include<ClassB, DTO_B>();

        //Mapper.CreateMap<ClassA, DTO_A>();

        //Mapper.CreateMap<ClassB, DTO_B>();

        //Mapper.AssertConfigurationIsValid();

        public CustomProfile()
        {
            //CreateMap<Data.Personne, Models.Personne>()
            //     .Include<Data.User, Models.User>();

            // Define your mapping configuration

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Data.Personne, Models.Personne>();
            //   // cfg.CreateMap<Data.User, Models.User>();
            //cfg.CreateMap<ProjectMaCaisseAPI_V01.Data.User, ProjectMaCaisseAPI_V01.Models.User>();

            //    cfg.CreateMap<Models.Personne, Data.Personne> ();
            //    cfg.CreateMap<Models.User, Data.User>();
            //});

            CreateMap<ProjectMaCaisseAPI_V01.Models.UserDto, ProjectMaCaisseAPI_V01.Data.User>();
            CreateMap<ProjectMaCaisseAPI_V01.Data.User, ProjectMaCaisseAPI_V01.Models.UserDto>();

        }

    }
}
