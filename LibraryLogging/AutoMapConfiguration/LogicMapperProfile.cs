using AutoMapper;
using DAL.Data;
using LibraryLogging.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// map ALL logic to  data object and vs 
/// </summary>
namespace WpfStudy.AutoMap
{
    /// <summary>
    /// Map Employee and EmployeeDTO
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile():base()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.AddressDTO, act => act.MapFrom(src => src.Address))
                .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department));

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<Address, AddressDTO>().ReverseMap();

            //    cfg.CreateMap<Employee, EmployeeDTO>()
            //    .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.AddressDTO, act => act.MapFrom(src => src.Address))
            //    .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department));
            //});
        }
    }
    /// <summary>
    /// Map Company-CompanyData
    /// </summary>
    public class CompanyProfile:Profile
    {

    }
    /// <summary>
    /// Map BaseOutlet-OutletData
    /// </summary>
    public class OutletProfile:Profile
    {
       public OutletProfile():base()
        {
            CreateMap<BaseOutlet, OutletData>();
        }
        

    }
   
    public class OrderProfile:Profile
    {
        public OrderProfile():base()
        {
            CreateMap<Order, OrderDTO>()

                   .ForMember(dest => dest.OrderId, action => action.MapFrom(source => source.OrderNo))

                   //Customer is a Complex type, so Map Customer to Simple type using For Member
                   .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Customer.FullName))
                   .ForMember(dest => dest.Postcode, act => act.MapFrom(src => src.Customer.Postcode))
                   .ForMember(dest => dest.MobileNo, act => act.MapFrom(src => src.Customer.ContactNo))
                   .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Customer.CustomerID))
                   .ReverseMap()
                   .ForMember(dest => dest.Customer,
                   act => act.MapFrom(src => new Customer(src.CustomerId, src.Name, src.Postcode, src.MobileNo)));
            
        }
    }
}
