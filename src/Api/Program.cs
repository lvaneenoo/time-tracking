var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddSingleton<IDeleteCommandFactory, DeleteCommandFactory>();
builder.Services.AddSingleton<IInsertCommandFactory, InsertCommandFactory>();

builder.Services.AddSingleton<ITimesheets, Timesheets>();

var app = builder.Build();

app.MapPost("/timesheet-entries/", TimesheetEntriesEndpoints.PostAsync);
app.MapDelete("/timesheet-entries/", TimesheetEntriesEndpoints.DeleteAsync);

app.MapGet("/timesheets/{date}", TimesheetsEndpoints.GetAsync);

app.Run();


public partial class Program { }
