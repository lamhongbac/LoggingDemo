using LibraryLogging.BusinessObjects;

namespace LibraryLogging
{
    public interface ICompanyBusinessProcess
    {
        CompanyProcessConfiguration CompanyProcessConfig { get; set; }
        string ChangeProcess(Company companyBusinessObject);
    }
}