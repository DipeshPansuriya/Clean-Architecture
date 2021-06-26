using Application_Command.Insert_Command;
using Application_Domain;
using AutoMapper;

namespace Application_Command.Profile_Mapping
{
    public class Demo_Customer_Pro_Map : Profile
    {
        public Demo_Customer_Pro_Map()
        {
            this.CreateMap<Demo_Customer_Inst_cmd, Demo_Customer>().ReverseMap();
            this.CreateMap<Demo_Customer_Upd_cmd, Demo_Customer>().ReverseMap();
        }
    }
}