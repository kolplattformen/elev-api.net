using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.News;
using System.Reflection;
using System.Text.Json;

namespace SkolplattformenElevApi
{
    public class FakeApi: IApi
    {
        private FakeData.FakeData _fakeData;

        public FakeApi()
        {

            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("fakedata.json"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();

                _fakeData = JsonSerializer.Deserialize<FakeData.FakeData>(result)!;
            }

       
        }

        public async Task LogInAsync(string email, string username, string password)
        {
            await Task.Delay(1000);
        }

        public Task<List<NewsListItem>> GetNewsItemListAsync(int itemsToGet = 5)
        {
            return Task.FromResult(new List<NewsListItem>());
        }

        public Task<NewsItem> GetNewsItemAsync(string path)
        {
            return Task.FromResult(new NewsItem());
        }

        public Task<ApiUser?> GetUserAsync()
        {
            return Task.FromResult(_fakeData.ApiUser);
        }

        public Task<List<Teacher>> GetTeachersAsync()
        {
            return Task.FromResult(_fakeData.Teachers);
        }

        public Task<SchoolDetails?> GetSchoolDetailsAsync(Guid schoolId)
        {
            return Task.FromResult(_fakeData.SchoolDetails.FirstOrDefault());
        }

        public Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
        {
            return Task.FromResult(_fakeData.TimeTable);
        }

        public Task<List<CalendarItem>> GetCalendarAsync(DateOnly date)
        {
            return Task.FromResult(_fakeData.CalendarItems);
        }

        public Task<List<PlannedAbsenceItem>> GetPlannedAbsenceListAsync()
        {
            return Task.FromResult(_fakeData.PlannedAbsenceItems);
        }

        public void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
        {
            Utils.Enrichers.EnrichTimetableWithTeachers(timetable, teachers);
        }

        public void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
        {
           Utils.Enrichers.EnrichTimetableWithCurriculum(timetable);
        }
    }
}
