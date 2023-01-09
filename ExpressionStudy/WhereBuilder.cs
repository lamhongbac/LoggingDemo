using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionStudy
{
    public static class FilterHanler<T>
    {
        public static bool IsStringOperator(ECompareOP compareOP)
        {
            return compareOP == ECompareOP.Contain ||
                compareOP == ECompareOP.Is || compareOP == ECompareOP.IsNot
                || compareOP == ECompareOP.StartsWith || compareOP == ECompareOP.EndsWith;
        }
        public static Func<T, bool> WhereFunction(FilterInFo<T> filterInfo)
        {
            Func<T, bool> compiledLambda = null;
            try
            {
                if (filterInfo.FilterElements.Length == 0)
                {
                    throw new Exception("Invalid_Para");
                }
                MethodInfo method;

                var parameterExpression =
        Expression.Parameter(typeof(T), "filterpara");

                BinaryExpression totalExpression = null;

                bool isFirstItem = true;
                //int i = 0;
                for (int i = 0; i < filterInfo.FilterElements.Length; i++)
                {
                    var filterElement = filterInfo.FilterElements[i];

                    var constant = Expression.Constant(filterElement.PropValue);
                    var property = Expression.Property(parameterExpression, filterElement.PropName);

                    BinaryExpression expression = null;
                    MethodCallExpression mexpression = null;
                    BinaryExpression nullCheckexpression = null;

                    switch (filterElement.FilterOP)
                    {
                        case ECompareOP.Equal:
                            expression = Expression.Equal(property, constant);
                            break;
                        case ECompareOP.EqualAndGreater:
                            expression = Expression.GreaterThanOrEqual(property, constant);
                            break;
                        case ECompareOP.EqualAndLessthan:
                            expression = Expression.LessThanOrEqual(property, constant);

                            break;
                        case ECompareOP.Greater:
                            expression = Expression.GreaterThan(property, constant);

                            break;
                        case ECompareOP.Lessthan:
                            expression = Expression.LessThan(property, constant);

                            break;
                        case ECompareOP.Is:
                            //var nullCheck = Expression.NotEqual(property, Expression.Constant(null, typeof(object)));
                            nullCheckexpression = Expression.Equal(property, Expression.Constant(null, typeof(object)));
                            break;

                        case ECompareOP.IsNot:
                            nullCheckexpression = Expression.NotEqual(property, Expression.Constant(null, typeof(object)));
                            expression = Expression.NotEqual(property, constant);

                            break;
                        case ECompareOP.Contain:
                            method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            mexpression = Expression.Call(property, method, constant);

                            break;
                        case ECompareOP.StartsWith:
                            method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                            mexpression = Expression.Call(property, method, constant);
                            break;



                        case ECompareOP.EndsWith:
                            method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                            mexpression = Expression.Call(property, method, constant);
                            break;
                        default:
                            expression = Expression.Equal(property, constant);

                            break;
                    }

                    if (isFirstItem)
                    {
                        isFirstItem = false;
                        totalExpression = expression;
                    }
                    else
                    {
                        //for second item
                        //i=1 linkElement[1]
                        var linkElement = filterInfo.LinkOPs[i - 1];
                        //BinaryExpression newExpression = (expression != null) ? expression : totalExpression;

                        switch (linkElement)
                        {

                            case ELinkOP.AND:

                                if (nullCheckexpression != null)
                                {
                                    totalExpression = Expression.AndAlso(nullCheckexpression, totalExpression);
                                }

                                if (mexpression != null)
                                {
                                    totalExpression = Expression.AndAlso(mexpression, totalExpression);
                                }
                                if (expression != null)
                                {
                                    totalExpression = Expression.AndAlso(expression, totalExpression);
                                }
                                break;
                            case ELinkOP.OR:
                                if (mexpression != null)
                                {
                                    totalExpression = Expression.Or(mexpression, totalExpression);
                                }
                                if (expression != null)
                                {
                                    totalExpression = Expression.Or(expression, totalExpression);
                                }
                                break;

                            default:
                                break;
                        }

                    }
                }
                var lambda = Expression.Lambda<Func<T,
        bool>>(totalExpression, parameterExpression);
                compiledLambda = lambda.Compile();
                // var result = _people.Where(compiledLambda).ToList();

                return compiledLambda;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw;
            }
            //return compiledLambda;
        }
        static Expression MyGreaterThan(Expression e1, Expression e2)
        {
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);
            return Expression.GreaterThan(e1, e2);
        }
        static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

}
