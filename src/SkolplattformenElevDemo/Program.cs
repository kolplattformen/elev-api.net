// See https://aka.ms/new-console-template for more information

using System.Text.Json;

Console.WriteLine("Hello, World!");

var s = await File.ReadAllTextAsync("./elev.json");
var elev = JsonSerializer.Deserialize<ElevInfo>(s);

var api = new SkolplattformenElev.Api();
await api.LogIn(elev.Email, elev.Username, elev.Password);


class ElevInfo
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}