using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Biodiv.Entities;
using BiodivApi.Entities.Enums;

namespace BiodivApi.Entities
{
    public class Specie : Entity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }
        [Required] [MaxLength(100)] public string TaxonomicGroup { get; set; }
        [Required] [MaxLength(50)] public string EnglishName { get; set; }
        [Required] [MaxLength(50)] public string ScientificName { get; set; }
        [Required] [MaxLength(150)] public string Habitat { get; set; }
        [Required] [Column(TypeName = "text")] public string Description { get; set; }
        [Required] public ConservationStatus Status { get; set; }
        [Required] [Column(TypeName = "text")] public string Threats { get; set; }

        public ICollection<LocalName> LocalNames { get; set; }
        public ICollection<SpeciePhoto> SpeciePhotos { get; set; }
        public ICollection<LocalDistribution> LocalDistributions { get; set; }
    }
}