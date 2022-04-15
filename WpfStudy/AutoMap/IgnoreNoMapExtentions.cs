using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStudy.AutoMap
{
  public static  class IgnoreNoMapExtentions
    {
        public static IMappingExpression<TSource,TDestination> IgnorgeNoMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression )
        {
            var sourceType = typeof(TSource);
            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                NoMapAttribute attribute = (NoMapAttribute)descriptor.Attributes[typeof(NoMapAttribute)];
                if (attribute != null)
                    expression.ForMember(property.Name, opt => opt.Ignore());

            }
            return expression;
        }
    }
}
