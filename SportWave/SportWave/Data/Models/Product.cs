using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportWave.Common.EntityValidationConstants.Product;

namespace SportWave.Data.Models
{
    public class Product
    {
        public Product()
        {
        this.variations = new HashSet<ProductVariation>();  
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; } = null!;

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [ForeignKey(nameof(ProductGender))]
        public string Gender { get; set; } = null!;
        public ProductGender ProductGender { get; set; } = null!;

        [Required]
        public string ImgUrl { get; set; } = null!;

        ICollection<ProductVariation> variations { get; set; }
    }
}