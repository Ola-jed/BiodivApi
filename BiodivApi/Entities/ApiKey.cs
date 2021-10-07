using System;
using System.ComponentModel.DataAnnotations;
using Biodiv.Entities;
using BiodivApi.Entities.Enums;

namespace BiodivApi.Entities
{
    public class ApiKey : Entity
    {
        [Required] [MaxLength(100)] public string EncodedKey { get; set; }
        [Required] public ApiKeyPermission Permission { get; set; }
        [Required] public DateTime ExpirationDate { get; set; }
    }
}