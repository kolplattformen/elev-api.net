using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using SkolplattformenElevApi.Models.Internal.StockholmAzureApi;

namespace SkolplattformenElevApi;

public partial class Api
{

    public async Task<UserData> GetUserAsync()
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url = "https://stockholm-o365-api.azurewebsites.net/api/User/getUser";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);



        var content = await response.Content.ReadAsStringAsync();

        var deserialized = JsonSerializer.Deserialize<GetUserResponse>(content);

        return deserialized.Data;
    }


    public async Task<List<Teacher>> GetTeachersAsync()
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url = "https://stockholm-o365-api.azurewebsites.net//api/SDS/student/teacher";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);



        var content = await response.Content.ReadAsStringAsync();

        var deserialized = JsonSerializer.Deserialize<GetTeachersResponse>(content);

        return deserialized.Data;
    }

    public async Task<SchoolDetails> GetSchoolAsync(string schoolId)
    {

        var token = await GetAzureApiAccessTokenAsync();

        var url = $"https://stockholm-o365-api.azurewebsites.net//api/soa/school/{schoolId}";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
            Headers =
            {
                { "Referer", "https://elevstockholm.sharepoint.com/" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
            },
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);



        var content = await response.Content.ReadAsStringAsync();


        var deserialized = JsonSerializer.Deserialize<GetSchoolResponse>(content);

        return deserialized.Data;
    }

}
