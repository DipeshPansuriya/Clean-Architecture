using Application_Command.Insert_Command.UserConfig;
using Application_Database;
using AutoMapper;

namespace Application_Command.Profile_Mapping.UserConfig
{
    public class User_Pro_Map : Profile
    {
        public User_Pro_Map()

        {
            CreateMap<User_Inst_cmd, TblUsermaster>().ReverseMap();

            CreateMap<User_Upd_cmd, TblUsermaster>().ReverseMap();

            CreateMap<User_Del_cmd, TblUsermaster>().ReverseMap();
        }
    }
}