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

    internal class InsertCommandFactory : IInsertCommandFactory
    {
        public IStorageCommand Create(TrackedDate date, Period period, Comment comment)
        {
            return new InsertTimeSheetEntry(date, period, comment);
        }
    }

    internal class InsertTimeSheetEntry(TrackedDate date, Period period, Comment comment) : IStorageCommand
    {
        private static readonly Period FiveToSix = new(new TimeOnly(17, 0), new TimeOnly(17, 59));

        private readonly Comment _comment = comment;
        private readonly Period _period = period;
        private readonly TrackedDate _date = date;

        public Task<int> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_date == January2025.First && _period == FiveToSix && _comment == new Comment(""))
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

            services.RemoveAll<IInsertCommandFactory>();
            services.AddSingleton<IInsertCommandFactory>(new InsertCommandFactory());

            services.RemoveAll<ITimeSheets>();
            services.AddSingleton<ITimeSheets>(new InMemoryTimeSheets([January2025.First.NineToFive()]));
        });

        return base.CreateHost(builder);
    }
}
