using System.Text.Json.Serialization;

[JsonSerializable(typeof(PostTimeSheetEntryRequest[]))]
[JsonSerializable(typeof(TimeSheetEntryResource[]))]
[JsonSerializable(typeof(TimeSheetResource[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
