﻿// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using SkolplattformenElevApi;

Console.WriteLine("Hello, World!");

var s = await File.ReadAllTextAsync("./elev.json");
var elev = JsonSerializer.Deserialize<ElevInfo>(s);

var api = new Api();
await api.LogInAsync(elev.Email, elev.Username, elev.Password);


//Console.WriteLine("\n----- Logged in, waiting -----");
//var minutes = 65;
//while (minutes > 0)
//{
//    Console.WriteLine($"{minutes} minutes left.");
//    Task.Delay(1000 * 60).Wait();
//    minutes--;
//}

//Console.WriteLine("\n----- Done waiting -----");
//Console.ReadLine();
//await api.RefreshLoginAsync();

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

//var calendarItems = await api.GetCalendarAsync(DateOnly.FromDateTime(DateTime.Now));
var calendarItems = await api.GetCalendarAsync(new DateOnly(2022,8,30));
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

Console.WriteLine("\n------- Meals ---------");
var meals = await api.GetMealsAsync(2022, 37);

foreach (var meal in meals)
{
    Console.WriteLine($"{meal.Title} {meal.Description}");
}

Console.WriteLine("\n------- Kalendarium ---------");
var kalendarium = await api.GetKalendariumAsync();

foreach (var item in kalendarium)
{
    Console.WriteLine($"{item.Title} {item.Description}");
}


Console.WriteLine("\n------- Status ---------");
var status = api.GetStatusAll();
foreach (var st in status)
{
    Console.WriteLine($"{st.Key} {st.Value}");
}

return; // this just here to have a place to set a breakpoint

class ElevInfo
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}