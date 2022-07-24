using SkolplattformenElevApi.Models.Absence;
using SkolplattformenElevApi.Models.Calendar;
using SkolplattformenElevApi.Models.News;
using SkolplattformenElevApi.Models.Timetable;

namespace SkolplattformenElevApi;

public interface IApi
{
    Task LogInAsync(string email, string username, string password);
    Task<List<CalendarItem>> GetCalendarAsync(DateOnly date);
    Task<List<PlannedAbsence>?> GetPlannedAbsenceListAsync();
    Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5);
    Task<NewsItem> GetNewsItemAsync(string path);
    Task<List<LessonInfo>?> GetTimetableAsync(int year, int week);
}