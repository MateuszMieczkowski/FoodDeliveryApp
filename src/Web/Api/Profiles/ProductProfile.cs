using AutoMapper;
using Library.Entities;
using Web.Api.Models.ProductDtos;

namespace Web.Api.Profiles;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap().PreserveReferences();
		CreateMap<Product, ProductForUpdateDto>().ReverseMap().PreserveReferences();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap().PreserveReferences();
		CreateMap<ProductCategory, ProductCategoryForUpdateDto>().ReverseMap().PreserveReferences();
    }
}
