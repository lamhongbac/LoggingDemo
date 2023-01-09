using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionStudy
{
    /// <summary>
    /// cung cap info cho viec filter
    /// Filter info chưa trong no 1 danh sach cac filter element va toan tu link voi nhau
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilterInFo<T>
    {
        private List<EFiterElement> filtersElements;
        private List<ELinkOP> linkElements;


        public FilterInFo()
        {
            filtersElements = new List<EFiterElement>();
            linkElements = new List<ELinkOP>();
        }

        public void AddFirstFilter(string propName, ECompareOP compareOP, object propVal)
        {
            if (filtersElements.Count > 0)
                throw new Exception("InvalidPara");
            filtersElements.Add(new EFiterElement()
            {
                PropName = propName,
                FilterOP = compareOP,
                PropValue = propVal

            });
        }

        public void AddSecondFilter(ELinkOP linkOP, string propName, ECompareOP compareOP, object propVal)
        {
            if (filtersElements.Count == 0)
                throw new Exception("InvalidPara");
            filtersElements.Add(new EFiterElement()
            {
                PropName = propName,
                FilterOP = compareOP,
                PropValue = propVal

            });
            linkElements.Add(linkOP);
        }

        // public OrderByInfo<T> OrderByInfo { get; set; }

        /// <summary>
        /// su dung cau where de xay dung ra cu phap cua tung element
        /// thi du where="A=@A AND B>B@ OR C IN @C"\
        /// element1=prop=A operator= Value(@A)
        /// link1= AND
        /// element2=B , >, B@
        /// </summary>
        //public string WhereCondition { get; set; }
        //cung cap value cho viec xay dung contants
        //public Dictionary<string, object> Parametters { get; set; }
        //su dung filter element de xay dung expression
        public EFiterElement[] FilterElements { get => filtersElements.ToArray(); }
        //so luong phan tu cua LinkOKs tinh tu 0 tuong ung voi elment 1 cua
        //filter
        //Element[0] linkOPs[0] Element[1]
        public ELinkOP[] LinkOPs { get => linkElements.ToArray(); }
        /// <summary>
        /// tra ve theo yc
        /// </summary>
        public string OrderByCommand { get; set; }
        //public PageInfo ThisPageInfo { get; set; }
        public string ObjectName { get; }

        //public void BuildQueryInfo(SelectFieldInfo<T> selectFieldsInfo, SQLWhereHelper<T> whereHelper, OrderByInfo<T> orderInfo, PageInfo pageInfo);
        public virtual bool IsValid()
        {
            return true;
        }

    }

    public class EFiterElement
    {
        public string PropName { get; set; }

        public ECompareOP FilterOP { get; set; }
        public object PropValue { get; set; }
    }
    /// <summary>
    /// cac toan tu trong express
    /// </summary>
    public enum ECompareOP
    {
        Equal, //=
        EqualAndGreater,//>=
        EqualAndLessthan, //<=
        Greater,//>
        Lessthan,//<
        Contain, //Like % val%
        StartsWith, // Like "B"
        EndsWith,
        Is, // Is Null
        IsNot //Is Not Null


    }
    public enum ELinkOP
    {
        AND,
        OR

    }
   

}
