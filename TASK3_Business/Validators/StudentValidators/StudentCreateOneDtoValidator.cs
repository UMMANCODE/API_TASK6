using FluentValidation;
using TASK3_Business.Dtos.StudentDtos;

namespace TASK3_Business.Validators.StudentValidators {
  public class StudentCreateOneDtoValidator : AbstractValidator<StudentCreateOneDto> {
    private const int MaxFileSize = 2 * 1024 * 1024;
    public StudentCreateOneDtoValidator() {
      RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
      RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
      RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
      RuleFor(x => x.Address).MaximumLength(200);
      RuleFor(x => x.Phone).MaximumLength(12);
      RuleFor(x => x.BirthDate).NotEmpty().Must(BeAtLeast15YearsOld);
      RuleFor(x => x.GroupId).NotEmpty().GreaterThan(0);

      RuleFor(x => x).Custom((x, c) => {
        if (x.FirstName != null && !char.IsUpper(x.FirstName[0])) {
          c.AddFailure(nameof(x.FirstName), "FirstName must start with uppercase!");
        }
      });

      RuleFor(x => x).Custom((x, c) => {
        if (x.LastName != null && !char.IsUpper(x.LastName[0])) {
          c.AddFailure(nameof(x.LastName), "LastName must start with uppercase!");
        }
      });

      RuleFor(x => x).Custom((f, c) => {
        if (f.Photo != null && f.Photo.Length > MaxFileSize) {
          c.AddFailure(nameof(f.Photo), "Photo must be less than or eaual to 2MB");
        }
      });
    }

    private static bool BeAtLeast15YearsOld(DateTime? birthdate) {
      var today = DateTime.Today;
      if (birthdate == null) {
        return false;
      }
      var age = today.Year - birthdate.Value.Year;

      if (birthdate.Value.Date > today.AddYears(-age)) {
        age--;
      }
      return age >= 15;
    }
  }
}
