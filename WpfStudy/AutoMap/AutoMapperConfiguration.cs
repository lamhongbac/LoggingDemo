using AutoMapper;
using DAL.Data;
using LibraryLogging.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfStudy.ViewModel;

namespace LibraryLogging.AutoMap
{
    /// <summary>
    /// Lop thuc hien cau hinh mapping, 
    /// lop nay thuc te chay tren du an logic(map logic va data)
    /// va tren du an Presentation (map viewModel - logic)
    /// Ham Mapper.Configure chay 1 lan khi ung dung startUp (Register sigle)
    /// </summary>
  public  class AutoMapperConfiguration
    {
        private  Mapper autoMapper;

        public Mapper AutoMapper { get => autoMapper; set => autoMapper = value; }

        public void Configure()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDTO>().ReverseMap();

                cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.AddressDTO, act => act.MapFrom(src => src.Address))
                .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department)).ReverseMap();
               
                cfg.CreateMap<BaseOutlet, OutletData>().ReverseMap();
                cfg.CreateMap<OutletData, OutletViewModel>().ReverseMap();

                cfg.CreateMap<Order, OrderDTO>()

                    .ForMember(dest => dest.OrderId, action => action.MapFrom(source => source.OrderNo))

                    //Customer is a Complex type, so Map Customer to Simple type using For Member
                    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Customer.FullName))
                    .ForMember(dest => dest.Postcode, act => act.MapFrom(src => src.Customer.Postcode))
                    .ForMember(dest => dest.MobileNo, act => act.MapFrom(src => src.Customer.ContactNo))
                    .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Customer.CustomerID))
                    .ReverseMap()
                    .ForMember(dest => dest.Customer, 
                    act => act.MapFrom(src=>new Customer(src.CustomerId,src.Name,src.Postcode,src.MobileNo)));
            });
             AutoMapper = new Mapper(config);
        }
      
    }
}
