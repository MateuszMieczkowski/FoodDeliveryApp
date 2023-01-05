using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.ProductDtos;

public class ProductCategoryForUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
