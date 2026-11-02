using Microsoft.AspNetCore.Mvc.Testing;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Microsoft.Extensions.Hosting;

namespace TestFloor;

public class TimeTracking : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IDeleteCommandFactory>();
            services.AddSingleton<IDeleteCommandFactory>(new DeleteCommandFactory());

            services.RemoveAll<IInsertCommandFactory>();
            services.AddSingleton<IInsertCommandFactory>(new InsertCommandFactory());

            services.RemoveAll<ITimesheets>();
            services.AddSingleton<ITimesheets>(new January2025());
        });

        return base.CreateHost(builder);
    }

    internal class DeleteCommandFactory : IDeleteCommandFactory
    {
        public IStorageCommand Create(TrackedDate date, Period period)
        {
            return new DeleteTimesheetEntry(date, period);
        }
    }

    internal class InsertCommandFactory : IInsertCommandFactory
    {
        public IStorageCommand Create(TrackedDate date, Period period, Comment comment)
        {
            return new InsertTimesheetEntry(date, period, comment);
        }
    }
}
