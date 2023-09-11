using AutoMapper.Execution;
using MVCWeb.Models;
using System.Collections.Generic;

namespace MVCWeb.Helper.PopUp
{
    public class DemoData
    {
        public List<TransactionViewModel> CreateListData()
        {
            List<TransactionViewModel> datas = new List<TransactionViewModel>();
            for (int i = 0;i<5; i++)
            {
                TransactionViewModel data = new TransactionViewModel()
                {
                    Amount = i * 1000,
                    Bank = "VCB",
                    ID = i + 1,
                    Name = " Le van Nuoi " + i.ToString(),
                    Number = "10222" + i.ToString(),
                    SWIFTCode = (10222 + i).ToString()
                };
                datas.Add(data);
            }
            return datas;
        }
    }
}
