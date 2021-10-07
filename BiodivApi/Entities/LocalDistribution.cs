using System.ComponentModel.DataAnnotations;
using Biodiv.Entities;

namespace BiodivApi.Entities
{
    public class LocalDistribution : Entity
    {
        [Required] [MaxLength(50)] public string Place { get; set; }
        [Required] [MaxLength(150)] public string Image { get; set; }
        [Required] public int SpecieId { get; set; }
    }
}