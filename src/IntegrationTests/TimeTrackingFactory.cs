using Microsoft.AspNetCore.Mvc.Testing;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Microsoft.Extensions.Hosting;

using TestFloor;

public class TimeTrackingFactory : WebApplicationFactory<Program>
{
    internal class DeleteCommandFactory : IDeleteCommandFactory
    {
        public IStorageCommand Create(TrackedDate date, Period period)
        {
            return new DeleteTimeSheetEntry(date, period);
        }
    }

    internal class DeleteTimeSheetEntry(TrackedDate date, Period period) : IStorageCommand
    {
        private readonly Period _period = period;
        private readonly TrackedDate _date = date;

        public Task<int> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_date == January2025.First && _period == Entries.NineToFive[0].Period)
            {
                return Task.FromResult(1);
            }

            return Task.FromResult(0);
        }
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IDeleteCommandFactory>();
            services.AddSingleton<IDeleteCommandFactory>(new DeleteCommandFactory());

            services.RemoveAll<ITimeSheets>();
            services.AddSingleton<ITimeSheets>(new InMemoryTimeSheets([January2025.First.NineToFive()]));
        });

        return base.CreateHost(builder);
    }
}
