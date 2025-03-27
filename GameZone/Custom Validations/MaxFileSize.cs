using System.ComponentModel.DataAnnotations;

namespace GameZone.Custom_Validations
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed size is {_maxFileSize} Bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
