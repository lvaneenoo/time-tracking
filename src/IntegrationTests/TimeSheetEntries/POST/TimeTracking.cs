using Microsoft.AspNetCore.Mvc.Testing;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Microsoft.Extensions.Hosting;

namespace TimeSheetEntryTests.Creation;

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

            services.RemoveAll<ITimeSheets>();
            services.AddSingleton<ITimeSheets>(new InMemoryTimeSheets([January2025.First.NineToFive()]));
        });

        return base.CreateHost(builder);
    }

    internal class DeleteCommandFactory : IDeleteCommandFactory
    {
        public IStorageCommand Create(TrackedDate date, Period period)
        {
            throw new InvalidOperationException();
        }
    }

    internal class InsertCommandFactory : IInsertCommandFactory
    {
        public IStorageCommand Create(TrackedDate date, Period period, Comment comment)
        {
            return new InsertTimeSheetEntry(date, period, comment);
        }
    }
}
