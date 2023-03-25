using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using Xunit;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreatedAndModifiedValue()
    {
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        _hrDatabaseContext.LeaveTypes.Add(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
        leaveType.DateModified.ShouldNotBeNull();
    }
}
