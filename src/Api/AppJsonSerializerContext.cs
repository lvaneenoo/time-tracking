using System.Text.Json.Serialization;

[JsonSerializable(typeof(PostTimesheetEntryRequest[]))]
[JsonSerializable(typeof(TimesheetEntryResource[]))]
[JsonSerializable(typeof(TimesheetResource[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
