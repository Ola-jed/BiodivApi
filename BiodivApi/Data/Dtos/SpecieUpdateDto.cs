using System.ComponentModel.DataAnnotations;
using BiodivApi.Entities.Enums;

namespace BiodivApi.Data.Dtos
{
    public record SpecieUpdateDto
    {
        [Required] [MaxLength(50)] public string Name { get; init; }
        [Required] [MaxLength(100)] public string TaxonomicGroup { get; init; }
        [Required] [MaxLength(50)] public string EnglishName { get; init; }
        [Required] [MaxLength(50)] public string ScientificName { get; init; }
        [Required] [MaxLength(150)] public string Habitat { get; init; }
        [Required] public string Description { get; init; }
        [Required] public ConservationStatus Status { get; init; }
        [Required] public string Threats { get; init; }
    }
}