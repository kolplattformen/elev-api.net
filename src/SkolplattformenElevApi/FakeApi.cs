using SkolplattformenElevApi.Models.Absence;
using SkolplattformenElevApi.Models.Calendar;
using SkolplattformenElevApi.Models.News;
using SkolplattformenElevApi.Models.Timetable;

namespace SkolplattformenElevApi;

public class FakeApi : IApi
{
    public Task LogInAsync(string email, string username, string password)
    {
        return Task.CompletedTask;
    }

    public Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
    {
        throw new NotImplementedException();
    }

    public Task<List<PlannedAbsence>?> GetPlannedAbsenceListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5)
    {
        throw new NotImplementedException();
    }

    public Task<NewsItem> GetNewsItemAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task<List<LessonInfo>?> GetTimetableAsync(int year, int week)
    {
        throw new NotImplementedException();
    }
}