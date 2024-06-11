using FluentValidation;
using TASK3_Business.Dtos.GroupDtos;

namespace TASK3_Business.Validators.GroupValidators
{
  public class GroupCreateOneDtoValidator : AbstractValidator<GroupCreateOneDto>
  {
    public GroupCreateOneDtoValidator()
    {
      RuleFor(x => x.Name)
        .NotEmpty()
        .MinimumLength(4)
        .MaximumLength(5);
      RuleFor(x => x.Limit)
        .NotEmpty()
        .InclusiveBetween(5, 18);
    }
  }
}
