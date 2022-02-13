namespace LibraryLogging
{
    public interface ICompanyBusinessProcess
    {
        CompanyProcessConfiguration CompanyProcessConfig { get; set; }
        string ChangeProcess(CompanyBusinessObject companyBusinessObject);
    }
}