using System.ComponentModel.DataAnnotations;
using Biodiv.Entities;

namespace BiodivApi.Entities
{
    public class SpeciePhoto : Entity
    {
        [Required] [MaxLength(150)] public string Photo { get; set; }
        [Required] public int SpecieId { get; set; }
    }
}