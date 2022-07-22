using SkolplattformenElevApi;
using SkolplattformenElevApi.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorDemoApp.Data;

public class SkolplattformenService
{
    private readonly Api _api;
    private DateTime _loggedInTime = DateTime.MinValue;
    public string LoggedInName { get; set; } = string.Empty;
    public bool IsLoggedIn =>  _loggedInTime > DateTime.UtcNow.AddMinutes(-29);
    

    public  SkolplattformenService()
    {
        _api = new SkolplattformenElevApi.Api();
    }

    public async Task LogIn(string email, string username, string password)
    {
        await _api.LogIn(email, username, password);
        _loggedInTime = DateTime.UtcNow;
        LoggedInName = email;
    }

    public Task<List<NewsListItem>> GetNewsItemList()
    {
        return _api.GetNewsItemList();
    }

    public Task<NewsItem> GetNewsItem(string path)
    {
        return _api.GetNewsItem(path);
    }
}
