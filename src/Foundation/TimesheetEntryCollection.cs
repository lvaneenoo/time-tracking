using System.Collections.ObjectModel;

public class TimesheetEntryCollection(IList<TimesheetEntry> entries) : ReadOnlyCollection<TimesheetEntry>(entries)
{
}
