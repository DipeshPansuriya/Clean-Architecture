using Application_Command.Insert_Command.UserConfig;
using Application_Domain.UserConfig;
using AutoMapper;

namespace Application_Command.Profile_Mapping.UserConfig
{
    public class Role_Pro_Map : Profile
    {
        public Role_Pro_Map()

        {
            this.CreateMap<Role_Inst_cmd, role_cls>().ReverseMap();

            this.CreateMap<Role_Upd_cmd, role_cls>().ReverseMap();

            this.CreateMap<Role_Del_cmd, role_cls>().ReverseMap();
        }
    }
}