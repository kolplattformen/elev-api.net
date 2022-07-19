using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Graph;
using SkolplattformenElevApi.Models.MsGraph;
using SkolplattformenElevApi.Models.Timetable;

namespace SkolplattformenElevApi;

public partial class Api
{

    public async Task<string> GetMsGraphAccessTokenAsync()
    {
        var tokenResponse = await GetOauthTokenResponseAsync(OauthTokenResource.MsGraph);

        return tokenResponse.AccessToken;
    }


    private async Task<OauthTokenResponse> GetOauthTokenResponseAsync(string tokenResource)
    {
        var temp_url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/_api/SP.OAuth.Token/Acquire";
        var content = "{\"resource\":\"" + tokenResource + "\"}";
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                { "Odata-version", "3.0"},
                { "Referer", "https://elevstockholm.sharepoint.com/sites/skolplattformen/" },
                { "X-Scope", "8a22163c-8662-4535-9050-bc5e1923df48" },
                { "Accept", "application/json"},
                { "Origin", "https://elevstockholm.sharepoint.com" },
                { "X-RequestDigest", _formDigestValue}
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

        var obj = JsonSerializer.Deserialize<OauthTokenResponse>(temp_content, jsonSerializerOptions);

        return obj;

    }

    public async Task<string> GetMeAsync()
    {

        var accessToken = await GetMsGraphAccessTokenAsync();


        var graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider((requestMessage) => {
            requestMessage
                .Headers
                .Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return Task.CompletedTask;
        }));

        var me = await graphServiceClient.Me.Request().GetAsync();

        return me.DisplayName;
    }
}
