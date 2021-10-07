using System.ComponentModel.DataAnnotations;
using BiodivApi.Data.Dtos.Validation;
using Microsoft.AspNetCore.Http;

namespace BiodivApi.Data.Dtos
{
    public record SpeciePhotoUpdateDto
    {
        [Required]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(".jpg,.jpeg,.png,.webp")]
        public IFormFile Photo { get; init; }
    }
}