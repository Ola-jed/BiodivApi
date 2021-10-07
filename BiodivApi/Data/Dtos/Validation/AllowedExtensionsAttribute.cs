using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BiodivApi.Data.Dtos.Validation
{
    public class AllowedExtensionsAttribute: ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string extensions)
        {
            _extensions = extensions.Split(",");
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return new ValidationResult("No file detected");
            }

            var extension = Path.GetExtension(file.FileName);
            return _extensions.Contains(extension)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage());
        }

        private string GetErrorMessage()
        {
            return $"This extension is not allowed";
        }
    }
}