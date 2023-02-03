using AutoMapper;
using DAL.Data;
using MVCWeb.Models;
using System.Collections.Generic;

namespace MVCWeb.DataServiceFW
{
    public class StoreDataHandler
    {
        DataService _dataservice;
        IMapper _mapper;
        public StoreDataHandler(DataService dataservice, IMapper mapper)
        {
            _dataservice = dataservice;
            _mapper = mapper;
        }
        public List<StoreViewModel> GetStoreViewModel()
        {
            List<StoreData> Data =_dataservice.GetStoreData();
            List<StoreViewModel> viewModels = _mapper.Map<List<StoreViewModel>>(Data);
            return viewModels;

        }
    }
}
