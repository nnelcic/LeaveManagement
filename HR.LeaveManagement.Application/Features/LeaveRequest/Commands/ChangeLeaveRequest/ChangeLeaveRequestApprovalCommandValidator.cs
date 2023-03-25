using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest;

public class ChangeLeaveRequestApprovalCommandValidator : 
    AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(x => x.Approved)
            .NotNull()
            .WithMessage("Approval status cannot be null");
    }
}
