using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler
    : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }



    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);

        leaveRequest.Cancelled = true;

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully.",
                Subject = "Leave Request Updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
            throw;
        }

        return Unit.Value;
    }
}
