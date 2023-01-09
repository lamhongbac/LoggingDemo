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
        //public class EFiterElement
        //{
        //    public string PropName { get; set; }

        //    public ECompareOP FilterOP { get; set; }
        //    public object PropValue { get; set; }
        //}
        //public static Func<T, bool> WhereFunction(FilterInFo<T> filterInfo)
        //{
        //    Func<T, bool> compiledLambda = null;
        //    try
        //    {
        //        if (filterInfo.FilterElements.Length == 0)
        //        {
        //            throw new Exception("Invalid_Para");
        //        }
        //        MethodInfo method;

        //        var parameterExpression =
        //Expression.Parameter(typeof(T), "filterpara");

        //        BinaryExpression totalExpression = null;

        //        bool isFirstItem = true;
        //        //int i = 0;
        //        for (int i = 0; i < filterInfo.FilterElements.Length; i++)
        //        {
        //            var filterElement = filterInfo.FilterElements[i];

        //            var constant = Expression.Constant(filterElement.PropValue);
        //            var property = Expression.Property(parameterExpression, filterElement.PropName);

        //            BinaryExpression expression = null;
        //            MethodCallExpression mexpression = null;
        //            BinaryExpression nullCheckexpression = null;

        //            switch (filterElement.FilterOP)
        //            {
        //                case ECompareOP.Equal:
        //                    expression = Expression.Equal(property, constant);
        //                    break;
        //                case ECompareOP.EqualAndGreater:
        //                    expression = Expression.GreaterThanOrEqual(property, constant);
        //                    break;
        //                case ECompareOP.EqualAndLessthan:
        //                    expression = Expression.LessThanOrEqual(property, constant);

        //                    break;
        //                case ECompareOP.Greater:
        //                    expression = Expression.GreaterThan(property, constant);

        //                    break;
        //                case ECompareOP.Lessthan:
        //                    expression = Expression.LessThan(property, constant);

        //                    break;
        //                case ECompareOP.Is:
        //                    //var nullCheck = Expression.NotEqual(property, Expression.Constant(null, typeof(object)));
        //                    nullCheckexpression = Expression.Equal(property, Expression.Constant(null, typeof(object)));
        //                    break;

        //                case ECompareOP.IsNot:
        //                    nullCheckexpression = Expression.NotEqual(property, Expression.Constant(null, typeof(object)));
        //                    expression = Expression.NotEqual(property, constant);

        //                    break;
        //                case ECompareOP.Contain:
        //                    method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //                    mexpression = Expression.Call(property, method, constant);

        //                    break;
        //                case ECompareOP.StartsWith:
        //                    method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        //                    mexpression = Expression.Call(property, method, constant);
        //                    break;



        //                case ECompareOP.EndsWith:
        //                    method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        //                    mexpression = Expression.Call(property, method, constant);
        //                    break;
        //                default:
        //                    expression = Expression.Equal(property, constant);

        //                    break;
        //            }

        //            if (isFirstItem)
        //            {
        //                isFirstItem = false;
        //                totalExpression = expression;
        //            }
        //            else
        //            {
        //                //for second item
        //                //i=1 linkElement[1]
        //                var linkElement = filterInfo.LinkOPs[i - 1];
        //                //BinaryExpression newExpression = (expression != null) ? expression : totalExpression;

        //                switch (linkElement)
        //                {

        //                    case ELinkOP.AND:

        //                        if (nullCheckexpression != null)
        //                        {
        //                            totalExpression = Expression.AndAlso(nullCheckexpression, totalExpression);
        //                        }

        //                        if (mexpression != null)
        //                        {
        //                            totalExpression = Expression.AndAlso(mexpression, totalExpression);
        //                        }
        //                        if (expression != null)
        //                        {
        //                            totalExpression = Expression.AndAlso(expression, totalExpression);
        //                        }
        //                        break;
        //                    case ELinkOP.OR:
        //                        if (mexpression != null)
        //                        {
        //                            totalExpression = Expression.Or(mexpression, totalExpression);
        //                        }
        //                        if (expression != null)
        //                        {
        //                            totalExpression = Expression.Or(expression, totalExpression);
        //                        }
        //                        break;

        //                    default:
        //                        break;
        //                }

        //            }
        //        }
        //        var lambda = Expression.Lambda<Func<T,
        //bool>>(totalExpression, parameterExpression);
        //        compiledLambda = lambda.Compile();
        //        // var result = _people.Where(compiledLambda).ToList();

        //        return compiledLambda;
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = ex.Message;
        //        throw;
        //    }
        //    //return compiledLambda;
        //}
        static Expression<Func<T, bool>> GetExpression<T>(string propertyName, string propertyValue)
        {
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        //static Expression<Func<T, bool>> GetExExpression<T>(Dictionary<string,object> myDictionary)
        //{
        //    foreach (KeyValuePair<string, object> kvp in myDictionary)
        //    {
        //        string propertyName = kvp.Key;
        //        object propertyValue = kvp.Value;

        //        var parameterExp = Expression.Parameter(typeof(T), "type");
        //        //tao ra 1 expression propery name thuoc ve type T
        //        //Propety: Type-prop
        //        var propertyExp = Expression.Property(parameterExp, propertyName);
        //        //Lay ra value=> tao ra Contants kieu string


        //        //neu kieu du lieu la string thi dung contain
        //        Expression containsMethodExp;
        //        MethodInfo method;
        //        if (propertyName=="Name")
        //        {

        //          var  nameValue = Expression.Constant(propertyValue, typeof(string));
        //            method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //             containsMethodExp = Expression.Call(propertyExp, method, nameValue);

        //        }
        //        if (propertyName == "Age")
        //        {
        //            var ageValue = Expression.Constant(propertyValue, typeof(int));
        //            method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //            containsMethodExp = Expression.Call(propertyExp, method, nameValue);
        //        }
        //            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        //    }
        //    //var parameterExp = Expression.Parameter(typeof(T), "type");
        //    //var propertyExp = Expression.Property(parameterExp, propertyName);





        //}
    }
    class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
