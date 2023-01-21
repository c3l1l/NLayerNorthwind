using AutoMapper;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        { 
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductAddDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryAddDto, Category>().ForMember(x => x.Picture,opt=>opt.Ignore());
            CreateMap<CategoryUpdateDto, Category>().ForMember(x => x.Picture,opt=>opt.Ignore());

            CreateMap<ProductsWithCategoryDto, Product>().ReverseMap();
          
        }
    }
}
