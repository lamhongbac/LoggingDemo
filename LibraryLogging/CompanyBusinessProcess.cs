using DAL;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryLogging
{
    public class CompanyBusinessProcess : ICompanyBusinessProcess
    {
        ILogger<CompanyBusinessProcess> _logger;
        CompanyProcessConfiguration _companyProcessConfiguration;
        public CompanyBusinessProcess(CompanyProcessConfiguration companyProcessConfiguration,
             ILogger<CompanyBusinessProcess> logger, IMyDAL<CompanyData> dal)
        {
            _companyProcessConfiguration = companyProcessConfiguration;
            _logger = logger; _dal = dal;
        }
        IMyDAL<CompanyData> _dal;
        public string ChangeProcess(CompanyBusinessObject companyBusinessObject)
        {
            string mess = string.Empty;
            if (_companyProcessConfiguration.CompanyType!=1)
            {
                mess = "wrong company type"; 
                _logger.LogError(mess, _companyProcessConfiguration.CompanyType);
                return mess;
            }
            //after process logic ==> udpate data
            CompanyData data = new CompanyData()
            {
                ID = companyBusinessObject.ID,
                Adress = companyBusinessObject.Adress,
                Name = companyBusinessObject.Name
            };

            if (_dal.UpdateData(data) > 0)
            {
                mess = "update success";
                
            }
            else
            {
                mess = "update false";
            }
            _logger.LogInformation(mess);
            return mess;
        }
    }
}
