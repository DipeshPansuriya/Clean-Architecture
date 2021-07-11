using Application_Command.Insert_Command.UserConfig;
using Application_Domain.UserConfig;
using AutoMapper;

namespace Application_Command.Profile_Mapping.UserConfig
{
    public class Right_Pro_Map : Profile
    {
        public Right_Pro_Map()

        {
            this.CreateMap<Right_Inst_cmd, rights_cls>().ReverseMap();

            this.CreateMap<Right_Upd_cmd, rights_cls>().ReverseMap();

            this.CreateMap<Right_Del_cmd, rights_cls>().ReverseMap();
        }
    }
}