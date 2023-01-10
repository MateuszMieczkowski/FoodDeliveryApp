using AutoMapper;
using Library.Entities;
using Library.Models.ProductDtos;

namespace Library.Profiles;

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
