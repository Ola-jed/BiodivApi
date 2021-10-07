using System.Collections.Generic;
using Biodiv.Entities;
using BiodivApi.Entities;
using BiodivApi.Entities.Enums;

namespace BiodivApi.Data.Dtos
{
    public record SpecieWithPhotosReadDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string TaxonomicGroup { get; init; }
        public string EnglishName { get; init; }
        public string ScientificName { get; init; }
        public string Habitat { get; init; }
        public string Description { get; init; }
        public ConservationStatus Status { get; init; }
        public string Threats { get; init; }
        public ICollection<SpeciePhoto> SpeciePhotos { get; init; }
    }
}