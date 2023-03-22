using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveTypeCommand;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(x => x.DefaultDays)
            .GreaterThan(1).WithMessage("{PropertyName} cannot exceed 100")
            .LessThan(100).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(x => x)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");

        _leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }

    private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
