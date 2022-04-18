using DAL;
using DAL.Data;
using LibraryLogging.BusinessObjects;
using Microsoft.Extensions.Logging;
using System;

namespace LibraryLogging
{
    public class CompanyBusinessProcess : ICompanyBusinessProcess
    {
        ILogger<CompanyBusinessProcess> logger;
        
        public CompanyBusinessProcess(
             ILogger<CompanyBusinessProcess> _logger,
             IMyDAL<CompanyData> _dal,
             CompanyProcessConfiguration _companyProcessConfiguration)
        {

            logger = _logger;
            dal = _dal;
            companyProcessConfiguration = _companyProcessConfiguration;
        }

        CompanyProcessConfiguration companyProcessConfiguration;
        public CompanyProcessConfiguration CompanyProcessConfig 
        { 
            get => companyProcessConfiguration;
            set => companyProcessConfiguration=value; 
        }
        IMyDAL<CompanyData> _dal;
        public IMyDAL<CompanyData> dal { 
            get => _dal; set => _dal = value; 
        }

        public string ChangeProcess(Company companyBusinessObject)
        {
            string mess = string.Empty;
            if (companyProcessConfiguration.CompanyType!=1)
            {
                mess = "wrong company type"; 
                logger.LogError(mess, companyProcessConfiguration.CompanyType);
                return mess;
            }
            //after process logic ==> udpate data
            CompanyData data = new CompanyData()
            {
                ID = companyBusinessObject.ID,
                Adress = companyBusinessObject.Adress,
                Name = companyBusinessObject.Name
            };

            if (dal.UpdateData(data) > 0)
            {
                mess = "update success";
                
            }
            else
            {
                mess = "update false";
            }
            logger.LogInformation(mess);
            return mess;
        }
    }
}
