using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models.MsGraph;
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
internal class OauthTokenResponse
{
    [JsonPropertyName("@odata.context")]
    public string OdataContext { get; set; }

    [JsonPropertyName("@odata.type")]
    public string OdataType { get; set; }

    [JsonPropertyName("@odata.id")]
    public string OdataId { get; set; }

    [JsonPropertyName("@odata.editLink")]
    public string OdataEditLink { get; set; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_on")]
    public string ExpiresOn { get; set; }

    [JsonPropertyName("id_token")]
    public object IdToken { get; set; }

    [JsonPropertyName("resource")]
    public string Resource { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}

internal static class OauthTokenResource
{
    public static string Bing => "https://www.bing.com";
    public static string OutlookSearch => "https://outlook.office365.com/search";
    public static string Loki => "https://loki.delve.office.com";
    public static string MsGraph => "https://graph.microsoft.com";

}