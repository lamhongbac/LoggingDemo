using AutoMapper;
using NewsCMS.Models;
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
            CreateMap<CKEditor, NewsData>().ReverseMap();
        }
    }
}
