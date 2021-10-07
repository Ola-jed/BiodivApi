using System.ComponentModel.DataAnnotations;
using BiodivApi.Data.Dtos.Validation;
using Microsoft.AspNetCore.Http;

namespace BiodivApi.Data.Dtos
{
    public record LocalDistributionCreateDto
    {
        [Required] [MaxLength(50)] public string Place { get; init; }
        [Required]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(".jpg,.jpeg,.png,.webp")]
        public IFormFile Image { get; init; }
    }
}