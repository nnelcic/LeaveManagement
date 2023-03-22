using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveTypeCommand;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaverTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);
        
        if (leaverTypeToDelete == null)
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);

        await _leaveTypeRepository.DeleteAsync(leaverTypeToDelete);

        return Unit.Value;
    }
}
