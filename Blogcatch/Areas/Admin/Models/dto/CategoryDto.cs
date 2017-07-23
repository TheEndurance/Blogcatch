using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogcatch.Areas.Admin.Models.dto
{
    public class CategoryDto : IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.Length <= 2 || Name.Length > 50)
            {
                yield return new ValidationResult(
                    "Category name must be greater than 2 characters and less than 50 characters",
                    new[] {nameof(this.Name)});
            }

        }

    }
}