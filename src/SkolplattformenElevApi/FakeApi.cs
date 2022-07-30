using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.News;

namespace SkolplattformenElevApi
{
    public class FakeApi: IApi
    {
        public async Task LogInAsync(string email, string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5)
        {
            throw new NotImplementedException();
        }

        public async Task<NewsItem> GetNewsItemAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiUser?> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Teacher>> GetTeachersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
