using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public int LeaveTypeId { get; set; }
}
