using AutoMapper;
using Ckeditor5AspNetCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsCMS.Data
{
    public class MapperDefinition : Profile
    {
        public MapperDefinition() : base()
        {
            CreateMap<NewsModel, NewsData>().ReverseMap();
        }
    }
}
