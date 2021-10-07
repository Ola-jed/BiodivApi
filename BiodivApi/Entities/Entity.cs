using System.ComponentModel.DataAnnotations;

namespace Biodiv.Entities
{
    public abstract class Entity
    {
        [Key] public int Id { get; set; }
    }
}