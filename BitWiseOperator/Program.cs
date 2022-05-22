using System;
using System.Collections.Generic;

namespace BitWiseOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoBusinessObject BO = new DemoBusinessObject();
            List<ACL> myACL = new List<ACL>() { ACL.View, ACL.Edit, ACL.MarkDel };
            BO.SetRight(myACL);
            ACL acl = ACL.Edit;
            if (BO.HasRight(acl))
            {
                Console.WriteLine("you have right [{0}] on BO", acl.ToString());
            }
            else
                {
                Console.WriteLine("you do not have right [{0}] on BO", acl.ToString());

            }
        }
    }
}
