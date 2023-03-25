using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands;

public class CancelLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
