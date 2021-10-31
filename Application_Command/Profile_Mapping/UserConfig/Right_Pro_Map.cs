using Application_Command.Insert_Command.UserConfig;
using Application_Database;
using AutoMapper;

namespace Application_Command.Profile_Mapping.UserConfig
{
    public class Right_Pro_Map : Profile
    {
        public Right_Pro_Map()

        {
            CreateMap<Right_Inst_cmd, TblRightmaster>().ReverseMap();

            CreateMap<Right_Upd_cmd, TblRightmaster>().ReverseMap();

            CreateMap<Right_Del_cmd, TblRightmaster>().ReverseMap();
        }
    }
}