// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using SkolplattformenElevApi;

Console.WriteLine("Hello, World!");

var s = await File.ReadAllTextAsync("./elev.json");
var elev = JsonSerializer.Deserialize<ElevInfo>(s);

var api = new Api();
await api.LogInAsync(elev.Email, elev.Username, elev.Password);

var itemList = await api.GetNewsItemListAsync(5);

Console.WriteLine("----- News -----");
foreach (var newsListItem in itemList)
{
    Console.WriteLine($"{newsListItem.Title} | {newsListItem.ModifiedBy} | {newsListItem.Path}");
}

await api.GetNewsItemAsync(itemList[1].Path);

Console.WriteLine("----- Planned Absence -----");

var absenceList = await api.GetPlannedAbsenceListAsync();
foreach (var a in absenceList)
{
    Console.WriteLine($"{a.DateTimeFrom.Date} {a.ReasonDescription} ({a.Reporter})");
}

Console.WriteLine("----- Timetable ------");


var lessonInfo = await api.GetTimetableAsync(2022, 37);

foreach (var info in lessonInfo)
{
    Console.WriteLine($"{info.DayOfWeekNumber} {info.TimeStart}-{info.TimeEnd}: {info.Texts[0]} {info.Texts[1]} {info.Texts[2]} ");
}


var calendar = await api.GetCalendarAsync(DateOnly.FromDateTime(DateTime.Now));
calendar = await api.GetCalendarAsync(DateOnly.FromDateTime(DateTime.Now.AddDays(1)));

class ElevInfo
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}