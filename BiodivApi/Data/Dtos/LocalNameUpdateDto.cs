using System.ComponentModel.DataAnnotations;

namespace BiodivApi.Data.Dtos
{
    public record LocalNameUpdateDto
    {
        [Required] [MaxLength(50)] public string Language { get; init; }
        [Required] [MaxLength(50)] public string Spelling { get; init; }
    }
}