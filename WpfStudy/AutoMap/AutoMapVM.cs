using AutoMapper;
using DAL.Data;
using LibraryLogging.AutoMapConfiguration;
using LibraryLogging.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDialogService;

namespace WpfStudy.AutoMap
{
    public class AutoMapVM
    {
        private readonly IDialogService dialogService;
        private IMapper mapper;
        public ICommand AutoMapCommand { get; }
        public ICommand AutoMapCommand2 { get; }
        public ICommand AutoMapCommand3 { get; }
        public AutoMapVM(IDialogService _dialogService)
        {
            AutoMapCommand = new ActionCommand(p => TestAutoMap());
            AutoMapCommand2 = new ActionCommand(p => TestAutoMap2());
            AutoMapCommand3 = new ActionCommand(p => TestAutoMap3());
            dialogService = _dialogService;
        }
        private void TestAutoMap()
        {
            //var config = new MapperConfiguration(cfg =>
            //    {
            //        cfg.CreateMap<Address, AddressDTO>().ReverseMap();

            //        cfg.CreateMap<Employee, EmployeeDTO>()
            //        .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name))
            //        .ForMember(dest=>dest.AddressDTO,act=>act.MapFrom(src=>src.Address))
            //        .ForMember(dest => dest.Dept, act => act.MapFrom(src => src.Department)).ReverseMap();
            //    });

            //Creating the source object
            Address empAddres = new Address()
            {
                City = "Mumbai",
                Stae = "Maharashtra",
                Country = "India"
            };
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = empAddres,
                Department = "IT"
            };
            ////Using automapper
            //var mapper = new Mapper(config);
            //EmployeeDTO empDTO = mapper.Map<EmployeeDTO>(emp);
            //string fullAddress= string.Format("Country{0}, City{1}", empDTO.AddressDTO.Country, empDTO.AddressDTO.City);

            IMapper mapper = AutoMappingConfig.GetMapper();
            EmployeeDTO empDTO = mapper.Map<EmployeeDTO>(emp);
            string fullAddress = string.Format("Country{0}, City{1}", empDTO.AddressDTO.Country, empDTO.AddressDTO.City);

            var viewModel = new DialogViewModel
                (string.Format("Employee {0},work at department:{1} with salary {2}, City {3}",
                empDTO.FullName, empDTO.Dept,empDTO.Salary,empDTO.AddressDTO.City));

            //(string.Format("Employee {0},work at department:{1} with salary {2}, City {4}",
            //    empDTO.FullName, empDTO.Dept, empDTO.Salary, empDTO.AddressDTO.City));
            bool? result = dialogService.ShowDialog(viewModel);
        }

        private void TestAutoMap2()
        {
            var config = new MapperConfiguration(cfg =>
            {                
                cfg.CreateMap<Employee, EmployeeDTO2>()
                .ForMember(dest => dest.City, act => act.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.State, act => act.MapFrom(src => src.Address.Stae));
            });

            //Creating the source object
            Address empAddres = new Address()
            {
                City = "Mumbai",
                Stae = "Maharashtra",
                Country = "India"
            };
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = empAddres,
                Department = "IT"
            };
            //Using automapper
            var mapper = new Mapper(config);
            EmployeeDTO2 empDTO = mapper.Map<EmployeeDTO2>(emp);
            //string fullAddress= string.Format("Country{0}, City{1}", empDTO.AddressDTO.Country, empDTO.AddressDTO.City);
            var viewModel = new DialogViewModel
                (string.Format("Employee {0},work at department:{1} with salary {2}, City {3}",
                empDTO.Name, empDTO.Department, empDTO.Salary, empDTO.City));

            //(string.Format("Employee {0},work at department:{1} with salary {2}, City {4}",
            //    empDTO.FullName, empDTO.Dept, empDTO.Salary, empDTO.AddressDTO.City));
            bool? result = dialogService.ShowDialog(viewModel);
        }

        private void TestAutoMap3()
        {
            Order OrderRequest = CreateOrderRequest();
            Mapper mapper = InitializeAutomapper();
            var orderDTOData = mapper.Map<Order, OrderDTO>(OrderRequest);
            //Step4: Print the OrderDTO Data
            StringBuilder sb = new StringBuilder();

            sb.Append("After Mapping - OrderDTO Data");
            sb.AppendLine("OrderId : " + orderDTOData.OrderId);
            sb.AppendLine("NumberOfItems : " + orderDTOData.NumberOfItems);
            sb.AppendLine("TotalAmount : " + orderDTOData.TotalAmount);
            sb.AppendLine("CustomerId : " + orderDTOData.CustomerId);
            sb.AppendLine("Name : " + orderDTOData.Name);
            sb.AppendLine("Postcode : " + orderDTOData.Postcode);
            sb.AppendLine("MobileNo : " + orderDTOData.MobileNo);
            var viewModel = new DialogViewModel(sb.ToString());
            bool? result = dialogService.ShowDialog(viewModel);

            //Step5: modify the OrderDTO data
            orderDTOData.OrderId = 10;
            orderDTOData.NumberOfItems = 20;
            orderDTOData.TotalAmount = 2000;
            orderDTOData.CustomerId = 5;
            orderDTOData.Name = "Smith";
            orderDTOData.Postcode = "12345";
            //Step6: Reverse Map
            mapper.Map(orderDTOData, OrderRequest);
            
            sb.Clear();

            //Step7: Print the Order Data
            sb.Append("After Reverse Mapping - Order Data/n");
            sb.AppendLine("OrderNo : " + OrderRequest.OrderNo);
            sb.AppendLine("NumberOfItems : " + OrderRequest.NumberOfItems);
            sb.AppendLine("TotalAmount : " + OrderRequest.TotalAmount);
            sb.AppendLine("CustomerId : " + OrderRequest.Customer.CustomerID);
            sb.AppendLine("FullName : " + OrderRequest.Customer.FullName);
            sb.AppendLine("Postcode : " + OrderRequest.Customer.Postcode);
            sb.AppendLine("ContactNo : " + OrderRequest.Customer.ContactNo);
            var viewModel2 = new SecondDialogVM(sb.ToString());
            result = dialogService.ShowDialog(viewModel2);
        }
        private static Order CreateOrderRequest()
        {
            return new Order
            {
                OrderNo = 101,
                NumberOfItems = 3,
                TotalAmount = 1000,
                Customer = new Customer()
                {
                    CustomerID = 777,
                    FullName = "James Smith",
                    Postcode = "755019",
                    ContactNo = "1234567890"
                },
            };
        }
        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>()
                    //OrderId is different so map them using For Member
                    .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.OrderNo))
                    //Customer is a Complex type, so Map Customer to Simple type using For Member
                    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Customer.FullName))
                    .ForMember(dest => dest.Postcode, act => act.MapFrom(src => src.Customer.Postcode))
                    .ForMember(dest => dest.MobileNo, act => act.MapFrom(src => src.Customer.ContactNo))
                    .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Customer.CustomerID))
                    .ReverseMap();
            });

            var mapper = new Mapper(config);
            return mapper;
        }

        public void TestAutoMapper4()
        { //Creating the source object
            Address empAddres = new Address()
            {
                City = "Mumbai",
                Stae = "Maharashtra",
                Country = "India"
            };
            Employee emp = new Employee
            {
                Name = "James",
                Salary = 20000,
                Address = empAddres,
                Department = "IT"
            };
            mapper = AutoMappingConfig.GetMapper();
            EmployeeDTO2 empDTO = mapper.Map<EmployeeDTO2>(emp);
        }
    }
}
