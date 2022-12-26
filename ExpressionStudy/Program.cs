using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var lambda = GetExpression<Customer>("Name", "Lam");
            Customer foo = new Customer { Name = "Lam Hong Bac",Age=20 };
            bool find = lambda.Compile()(foo);
            if (find)
            {
                Console.WriteLine("I found you!");
            }
        }
        static Expression<Func<T, bool>> GetExpression<T>(string propertyName, string propertyValue)
        {
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        static Expression<Func<T, bool>> GetExExpression<T>(Dictionary<string,object> myDictionary)
        {
            foreach (KeyValuePair<string, object> kvp in myDictionary)
            {
                string propertyName = kvp.Key;
                object propertyValue = kvp.Value;

                var parameterExp = Expression.Parameter(typeof(T), "type");
                //tao ra 1 expression propery name thuoc ve type T
                //Propety: Type-prop
                var propertyExp = Expression.Property(parameterExp, propertyName);
                //Lay ra value=> tao ra Contants kieu string
                

                //neu kieu du lieu la string thi dung contain
                Expression containsMethodExp;
                MethodInfo method;
                if (propertyName=="Name")
                {
                   
                  var  nameValue = Expression.Constant(propertyValue, typeof(string));
                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                     containsMethodExp = Expression.Call(propertyExp, method, nameValue);
                    
                }
                if (propertyName == "Age")
                {
                    var ageValue = Expression.Constant(propertyValue, typeof(int));
                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    containsMethodExp = Expression.Call(propertyExp, method, nameValue);
                }
                    return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
            }
            //var parameterExp = Expression.Parameter(typeof(T), "type");
            //var propertyExp = Expression.Property(parameterExp, propertyName);
            
            
            

            
        }
    }
    class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
