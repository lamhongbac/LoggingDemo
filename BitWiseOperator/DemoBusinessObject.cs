using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWiseOperator
{
    public class DemoBusinessObject
    {
        int yourACL;//= (int)ACL.View + (int)ACL.Edit + (int)ACL.MarkDel;

        public void SetRight(List<ACL> acls)
        {
            yourACL = 0;
            foreach(var item in acls)
            {
                yourACL += (int)item;
            }


        }

        /// <summary>
        /// Ham kiem tra xem co quyen hay khong
        /// thi du khi Set quyen la View+Edit+Del
        /// thi IsMyRight(Create)=>false
        /// </summary>
        /// <param name="yourRight"></param>
        /// <returns></returns>
        public bool HasRight(ACL yourRight)
        {
            //int total = 1 + 2 + 4 + 8 + 16 + 32;
            int right = (int)yourRight;

            int check = yourACL & right;
            return check > 0;
        }
        /// <summary>
        /// Tra ve so tong cua tung thanh phan ben trong enum
        /// </summary>
        /// <param name="enumtype"></param>
        /// <returns></returns>
        public int GetTotal(Type enumtype)
        {
            return 1;
        }
    }
    /// <summary>
    /// Mo ta cac option
    /// </summary>
    public enum ACL
    {
        NoAccess=1,
        View=2,
        Edit=4,
        Create=8,
        MarkDel=16,
        HarDel=32
    }
}
