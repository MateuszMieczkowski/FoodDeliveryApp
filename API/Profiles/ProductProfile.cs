using API.Models.ProductDtos;
using AutoMapper;
using Library.Entities;

namespace API.Profiles;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<Product, ProductForUpdateDto>().ReverseMap();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
		CreateMap<ProductCategory, ProductCategoryForUpdateDto>().ReverseMap();
    }
}
