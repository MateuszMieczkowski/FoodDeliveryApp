using AutoMapper;
using Library.Entities;
using Web.Api.Models.ProductDtos;

namespace Web.Api.Profiles;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap().PreserveReferences();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap().PreserveReferences();
    }
}
