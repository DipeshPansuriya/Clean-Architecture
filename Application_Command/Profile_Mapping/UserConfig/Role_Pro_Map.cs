using Application_Command.Insert_Command.UserConfig;
using Application_Database;
using AutoMapper;

namespace Application_Command.Profile_Mapping.UserConfig
{
    public class Role_Pro_Map : Profile
    {
        public Role_Pro_Map()

        {
            CreateMap<Role_Inst_cmd, TblRolemaster>().ReverseMap();

            CreateMap<Role_Upd_cmd, TblRolemaster>().ReverseMap();

            CreateMap<Role_Del_cmd, TblRolemaster>().ReverseMap();
        }
    }
}