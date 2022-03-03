using System.Collections.Generic;

namespace DAL
{
    public interface IMyDAL<T>
    {
        
        void CreateData(T data);
        int DeleteData(T data);
        T[] ReadData(string whereCondition, Dictionary<string, object> paras);
        IEnumerable<T> ReadData(object para);
        int UpdateData(T data);
    }
}