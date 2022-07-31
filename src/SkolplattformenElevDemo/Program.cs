// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using SkolplattformenElevApi;

Console.WriteLine("Hello, World!");

var s = await File.ReadAllTextAsync("./elev.json");
var elev = JsonSerializer.Deserialize<ElevInfo>(s);

var api = new FakeApi();
await api.LogInAsync(elev.Email, elev.Username, elev.Password);

Console.WriteLine("\n----- News -----");

var itemList = await api.GetNewsItemListAsync(5);

foreach (var newsListItem in itemList)
{
    Console.WriteLine($"{newsListItem.Title} | {newsListItem.ModifiedBy} | {newsListItem.Path}");
}

//await api.GetNewsItemAsync(itemList[1].Path);

Console.WriteLine("\n----- Planned Absence -----");

var absenceList = await api.GetPlannedAbsenceListAsync();
foreach (var a in absenceList)
{
    Console.WriteLine($"{a.DateTimeFrom.Date} {a.ReasonDescription} ({a.Reporter})");
}


Console.WriteLine("\n----- Calendar ------");

var calendarItems = await api.GetCalendarAsync(DateOnly.FromDateTime(DateTime.Now));
foreach (var item in calendarItems)
{
    Console.WriteLine($"{item.Start} {item.End} {item.Title}");
}


Console.WriteLine("\n----- User ------");
var user = await api.GetUserAsync();
Console.WriteLine(user.Name);

Console.WriteLine("\n----- School ------");

var school = await api.GetSchoolDetailsAsync(user.Schools.First().ExternalId);
Console.WriteLine($"{school.Name} ");

var teachers = await api.GetTeachersAsync();
Console.WriteLine("\n----- Teachers ------");
foreach (var teacher in teachers)
{
    Console.WriteLine($"{teacher.Firstname} {teacher.Lastname} {teacher.Email}");
}

Console.WriteLine("\n----- Timetable ------");

var lessonInfo = await api.GetTimetableAsync(2022, 37);

api.EnrichTimetableWithTeachers(lessonInfo, teachers);
api.EnrichTimetableWithCurriculum(lessonInfo);

foreach (var info in lessonInfo)
{
    Console.WriteLine($"{info.DayOfWeekNumber} {info.TimeStart}-{info.TimeEnd}: {info.SubjectName} {info.TeacherName} {info.Location} ");
}




return; // this just here to have a place to set a breakpoint

class ElevInfo
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}