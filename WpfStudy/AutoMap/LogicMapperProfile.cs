using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStudy.AutoMap
{
    /// <summary>
    /// map ALL logic va data object
    /// 
    /// </summary>
   public class LogicEmployeeMapperProfile:Profile
    {
        public LogicEmployeeMapperProfile():base()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDTO>().ReverseMap();

                cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.AddressDTO, act => act.MapFrom(src => src.Address))
                .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department));
            });
        }
    }
}
