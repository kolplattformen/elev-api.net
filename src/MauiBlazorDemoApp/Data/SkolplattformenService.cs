using SkolplattformenElevApi;
using SkolplattformenElevApi.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkolplattformenElevApi.Models;

namespace MauiBlazorDemoApp.Data;

public enum ApiKind
{
    Skolplattformen = 1,
    FakeData = 2
}

public class SkolplattformenService
{
    private IApi _api;
    private DateTime _loggedInTime = DateTime.MinValue;
    public string LoggedInName { get; set; } = string.Empty;
    public bool IsLoggedIn =>  _loggedInTime > DateTime.UtcNow.AddMinutes(-29);
    private ApiKind _apiKind = ApiKind.Skolplattformen;
    

    public  SkolplattformenService()
    {
        _api = new SkolplattformenElevApi.Api();
        
    }

    public void SelectApi(ApiKind apiKind)
    {
        _apiKind = apiKind;

        LogOut();
    }

    public void LogOut()
    {
        _loggedInTime = DateTime.MinValue;
        if (_apiKind == ApiKind.Skolplattformen)
        {
            _api = new Api();
        }
        else
        {
            _api = new FakeApi();
        }
    }

    public async Task LogInAsync(string email, string username, string password)
    {
        await _api.LogInAsync(email, username, password);
        _loggedInTime = DateTime.UtcNow;
        var user = await _api.GetUserAsync();
        LoggedInName = user?.Name ?? email;
    }

    public Task<List<NewsListItem>> GetNewsItemList()
    {
        return _api.GetNewsItemListAsync();
    }

    public Task<NewsItem> GetNewsItem(string path)
    {
        return _api.GetNewsItemAsync(path);
    }

    public Task<List<Teacher>> GetTeachersAsync()
    {
        return _api.GetTeachersAsync();
    }

    public Task<List<TimeTableLesson>> GetTimetableAsync(int year, int week)
    {
        return _api.GetTimetableAsync(year, week);
    }
   
}
