using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.ProductDtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
