using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogging.AutoMapConfiguration
{
    /// <summary>
    /// Using profile
    /// 
    /// StartUp: AutoMappingConfig.Configure
    /// </summary>
    public static class AutoMappingConfig
    {
        private static IMapper mapper;
        public static IMapper GetMapper()
        {
            if (mapper == null)
            {
                mapper = Configure();
            }
            return mapper;
        }
        private static IMapper Configure()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmployeeProfile()) ;
                cfg.AddProfile(new CompanyProfile());
                cfg.AddProfile(new OutletProfile());
                cfg.AddProfile(new OrderProfile());
            });
            var mapper = config.CreateMapper();
            return mapper;
        }


        //private static bool _isInitialized;
        //public static Initialize()
        //{
        //    if (!_isInitialized)
        //    {
        //        Mapper.Initialize(cfg =>
        //        {
        //            cfg.AddProfile(new EmployeeProfile());
        //            cfg.AddProfile(new CompanyProfile());
        //            cfg.AddProfile(new OutletProfile());
        //            cfg.AddProfile(new OrderProfile());
        //        });
        //        _isInitialized = true;
        //    }
        //}
    }
}
