using System.Collections.ObjectModel;

public class TimeSheetEntryCollection(IList<TimeSheetEntry> list) : ReadOnlyCollection<TimeSheetEntry>(list)
{
}
