// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using SkolplattformenElevApi;

Console.WriteLine("Hello, World!");

var s = await File.ReadAllTextAsync("./elev.json");
var elev = JsonSerializer.Deserialize<ElevInfo>(s);

var api = new Api();
await api.LogIn(elev.Email, elev.Username, elev.Password);

var itemList = await api.GetNewsItemList();

Console.WriteLine("----- News -----");
foreach (var newsListItem in itemList)
{
    Console.WriteLine($"{newsListItem.Title} | {newsListItem.ModifiedBy} | {newsListItem.Path}");
}

await api.GetNewsItem(itemList[1].Path);

class ElevInfo
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}