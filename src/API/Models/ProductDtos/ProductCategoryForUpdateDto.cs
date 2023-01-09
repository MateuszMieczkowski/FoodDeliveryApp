using System.ComponentModel.DataAnnotations;

namespace API.Models.ProductDtos;

public class ProductCategoryForUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
