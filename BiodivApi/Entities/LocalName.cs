using System.ComponentModel.DataAnnotations;
using Biodiv.Entities;

namespace BiodivApi.Entities
{
    public class LocalName : Entity
    {
        [Required] [MaxLength(50)] public string Language { get; set; }
        [Required] [MaxLength(50)] public string Spelling { get; set; }
        [Required] public int SpecieId { get; set; }
    }
}