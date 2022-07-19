// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using SkolplattformenElevApi;

Console.WriteLine("Hello, World!");

var s = await File.ReadAllTextAsync("./elev.json");
var elev = JsonSerializer.Deserialize<ElevInfo>(s);

var api = new Api();
await api.LogIn(elev.Email, elev.Username, elev.Password);

var itemList = await api.GetNewsItemList(5);

Console.WriteLine("----- News -----");
foreach (var newsListItem in itemList)
{
    Console.WriteLine($"{newsListItem.Title} | {newsListItem.ModifiedBy} | {newsListItem.Path}");
}

await api.GetNewsItem(itemList[1].Path);

Console.WriteLine("---- Planned Absence -----");

await api.AbsenceSsoLogin();
var absenceList = await api.GetPlannedAbsenceList();
foreach (var a in absenceList)
{
    Console.WriteLine($"{a.DateTimeFrom.Date} {a.ReasonDescription} ({a.Reporter})");
}


class ElevInfo
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}