using AutoMapper;
using DAL.Data;
using MVCWeb.Models;
using System;

namespace MVCWeb.DataServiceFW
{
    public class DataMapper:Profile
    {
        public DataMapper() :base()
        {
            CreateMap<StoreData, StoreViewModel>().ReverseMap();
        }
    }
}
