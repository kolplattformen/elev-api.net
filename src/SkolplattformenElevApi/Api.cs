using System.Net;
using System.Text.RegularExpressions;

namespace SkolplattformenElevApi;

public partial class Api
{
    private readonly CookieContainer _cookieContainer;
    private readonly HttpClient _httpClient;
    private string _sharePointRequestGuid;
    private string _formDigestValue;
    public Api()
    {
        _cookieContainer = new CookieContainer();
        var httpClientHandler = new HttpClientHandler { CookieContainer = _cookieContainer, AllowAutoRedirect = false };
        _httpClient = new HttpClient(httpClientHandler);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"); ;
    }


 
    private string RegExp(string pattern, string source)
    {
        var reg = new Regex(pattern);
        var matches = reg.Matches(source);
        
        return matches[0].Groups[1].Value;
    }

    
}
