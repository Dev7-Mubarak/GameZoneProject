using System.ComponentModel.DataAnnotations;

namespace GameZone.Helpers.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);

                var IsAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

                if (!IsAllowed)
                {
                    return new ValidationResult($"Only {_allowedExtensions} are allowed!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
