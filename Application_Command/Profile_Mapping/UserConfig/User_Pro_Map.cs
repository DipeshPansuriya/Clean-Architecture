using Application_Command.Insert_Command.UserConfig;
using Application_Domain.UserConfig;
using AutoMapper;

namespace Application_Command.Profile_Mapping.UserConfig
{
    public class User_Pro_Map : Profile
    {
        public User_Pro_Map()

        {
            this.CreateMap<User_Inst_cmd, user_cls>().ReverseMap();

            this.CreateMap<User_Upd_cmd, user_cls>().ReverseMap();

            this.CreateMap<User_Del_cmd, user_cls>().ReverseMap();
        }
    }
}