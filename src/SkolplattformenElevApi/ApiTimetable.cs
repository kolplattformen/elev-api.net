using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models.Absence;
using SkolplattformenElevApi.Models.Timetable;

namespace SkolplattformenElevApi;

public partial class Api
{
    public async Task TimetableSsoLogin()
    {
        var temp_url = "https://fnsservicesso1.stockholm.se/sso-ng/saml-2.0/authenticate?customer=https://login001.stockholm.se&targetsystem=TimetableViewer";
        
        var temp_res = await _httpClient.GetAsync(temp_url);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var samlRequest = RegExp("\"SAMLRequest\\\" value=\\\"([^\\\"]*)\"", temp_content);
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("SAMLRequest", samlRequest),
        }));

        temp_content = await temp_res.Content.ReadAsStringAsync();
        var samlResponse = RegExp("\"SAMLResponse\\\" value=\\\"([^\\\"]*)\"", temp_content);
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("SAMLResponse", samlResponse),
        }));

        while (temp_res.Headers.Location != null)
        {
            temp_url = temp_res.Headers.Location?.ToString();
            temp_url = temp_url.StartsWith("/") ? "https://fns.stockholm.se" + temp_url : temp_url;
            temp_res = await _httpClient.GetAsync(temp_url);
        }
    }

    private async Task<(string,string)> GetTimetableUnitGuidAndPersonGuid()
    {
        var temp_url = "https://fns.stockholm.se/ng/api/services/skola24/get/personal/timetables";

        var content = "{\"getPersonalTimetablesRequest\":{\"hostName\":\"fns.stockholm.se\"}}";
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/timetable/timetable-viewer" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json"},
                { "Origin", "https://fns.stockholm.se" },
            },
            Content = new StringContent(content)
        };
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");


        var temp_res = await _httpClient.SendAsync(request);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var obj = JsonSerializer.Deserialize<TimetablePersonalTimetablesResponse>(temp_content, jsonSerializerOptions);

        var unitGuid = obj.Data.GetPersonalTimetablesResponse.StudentTimetables.FirstOrDefault().UnitGuid;
        var personGuid = obj.Data.GetPersonalTimetablesResponse.StudentTimetables.FirstOrDefault().PersonGuid;

        return (unitGuid, personGuid);
    }

    private async Task<string> GetTimetableRenderKey()
    {
        var temp_url = "https://fns.stockholm.se/ng/api/get/timetable/render/key";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/timetable/timetable-viewer/fns.stockholm.se/" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json"},
                { "Origin", "https://fns.stockholm.se" },
            },
        };

        var temp_res = await _httpClient.SendAsync(request);
        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var obj = JsonSerializer.Deserialize<TimetableRenderKeyResponse>(temp_content, jsonSerializerOptions);
        var key = obj.Data.Key;
        
        return key;
    }

    public async Task<List<LessonInfo>?> GetTimetable(int year, int week)
    {
        var (unitGuid, personGuid) = await GetTimetableUnitGuidAndPersonGuid();
        var key = await GetTimetableRenderKey();

        var content = "{\"renderKey\":\"" + key +  "\",\"host\":\"fns.stockholm.se\",\"unitGuid\":\"" + unitGuid + "\",\"startDate\":null,\"endDate\":null,\"scheduleDay\":0,\"blackAndWhite\":false,\"width\":1227,\"height\":1191,\"selectionType\":5,\"selection\":\"" + personGuid + "\",\"showHeader\":false,\"periodText\":\"\",\"week\":" + week + ",\"year\":" + year + ",\"privateFreeTextMode\":null,\"privateSelectionMode\":true,\"customerKey\":\"\"}";

        var temp_url = "https://fns.stockholm.se/ng/api/render/timetable";

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Referer", "https://fns.stockholm.se/ng/timetable/timetable-viewer/fns.stockholm.se/" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json"},
                { "Origin", "https://fns.stockholm.se" },
            },
            Content = new StringContent(content)
        };
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        var temp_res = await _httpClient.SendAsync(request);

        var temp_content = await temp_res.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var authorizeResponse = JsonSerializer.Deserialize<TimetableRenderResponse>(temp_content, jsonSerializerOptions);

        return authorizeResponse?.Data?.LessonInfo;
    }
}

