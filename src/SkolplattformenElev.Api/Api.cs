using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace SkolplattformenElev;

public class Api
{
    private readonly CookieContainer _cookieContainer;
    private readonly HttpClient _httpClient;

    public Api()
    {
        _cookieContainer = new CookieContainer();
        var httpClientHandler = new HttpClientHandler { CookieContainer = _cookieContainer, AllowAutoRedirect = false };
        _httpClient = new HttpClient(httpClientHandler);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"); ;
    }

    public async Task LogIn(string email, string username, string password)
    {

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var temp_url = "https://skolplattformen.stockholm.se/";
        var temp_res = await _httpClient.GetAsync(temp_url);

        temp_url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/";
        temp_res = await _httpClient.GetAsync(temp_url);

        temp_url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/_layouts/15/Authenticate.aspx?Source=%2Fsites%2Fskolplattformen%2F";
        temp_res = await _httpClient.GetAsync(temp_url);

        temp_url = "https://elevstockholm.sharepoint.com/_forms/default.aspx?ReturnUrl=%2fsites%2fskolplattformen%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252Fskolplattformen%252F&Source=cookie";
        temp_res = await _httpClient.GetAsync(temp_url);
        
        // temp_url="" 
        temp_url = temp_res.Headers.Location?.ToString();
        //TODO: Get nonce from above redirect
        var nonce = new Uri(temp_url).Query.Split("&").First(x => x.StartsWith("nonce=")).Split("=")[1];
        temp_res = await _httpClient.GetAsync(temp_url);

        // temp_url = "https://login.microsoftonline.com/e36726e9-4d94-4a77-be61-d4597f4acd02/oauth2/authorize?client_id=00000003-0000-0ff1-ce00-000000000000&response_mode=form_post&protectedtoken=true&response_type=code%20id_token&resource=00000003-0000-0ff1-ce00-000000000000&scope=openid&nonce=D183A6ABC1D1AC7C936304BB86DF8DAC910B49EBC59F2480-DAC0A994AB24A884A2F5B50818B527241CDB7D3431E47CA861C947015C5C1F6F&redirect_uri=https%3A%2F%2Felevstockholm.sharepoint.com%2F_forms%2Fdefault.aspx&state=OD0w&claims=%7B%22id_token%22%3A%7B%22xms_cc%22%3A%7B%22values%22%3A%5B%22CP1%22%5D%7D%7D%7D&wsucxt=1&cobrandid=11bd8083-87e0-41b5-bb78-0bc43c8a8e8a&client-request-id=7a323fa0-c071-4000-4218-a4696e60d3c2";
        temp_url = temp_res.Headers.Location?.ToString();
        temp_res = await _httpClient.GetAsync(temp_url);


        // temp_url = "https://login.microsoftonline.com/e36726e9-4d94-4a77-be61-d4597f4acd02/oauth2/authorize?client_id=00000003-0000-0ff1-ce00-000000000000&response_mode=form_post&protectedtoken=true&response_type=code%20id_token&resource=00000003-0000-0ff1-ce00-000000000000&scope=openid&nonce=D183A6ABC1D1AC7C936304BB86DF8DAC910B49EBC59F2480-DAC0A994AB24A884A2F5B50818B527241CDB7D3431E47CA861C947015C5C1F6F&redirect_uri=https%3A%2F%2Felevstockholm.sharepoint.com%2F_forms%2Fdefault.aspx&state=OD0w&claims=%7B%22id_token%22%3A%7B%22xms_cc%22%3A%7B%22values%22%3A%5B%22CP1%22%5D%7D%7D%7D&wsucxt=1&cobrandid=11bd8083-87e0-41b5-bb78-0bc43c8a8e8a&client-request-id=7a323fa0-c071-4000-4218-a4696e60d3c2&sso_reload=true";
        temp_url = $"{temp_url}&sso_reload=true";
        temp_res = await _httpClient.GetAsync(temp_url);
        var temp_content = await temp_res.Content.ReadAsStringAsync();
        var json = RegExp("\\$Config=(.*});", temp_content);
        var authStuff = JsonSerializer.Deserialize<AuthorizeStuff>(json, jsonSerializerOptions);

        temp_url = "https://login.live.com/Me.htm?v=3";
        temp_res = await _httpClient.GetAsync(temp_url);


        //TODO what is flowToken and OriginalRequest? FlowToken is sFT in the result of line 43 (sso_reload=true). OriginalRequest is sCtx. Regex "\$Config=(.*});"gm 
        // todo: Anropet innehåller en hel del headers. Kolla var de kommer ifrån. Verkar inte behövas
        var content =
            "{\"username\":\"" + email + "\",\"isOtherIdpSupported\":true,\"checkPhones\":false,\"isRemoteNGCSupported\":true,\"isCookieBannerShown\":false,\"isFidoSupported\":true,\"originalRequest\":\"" + authStuff.sCtx + "\",\"country\":\"SE\",\"forceotclogin\":false,\"isExternalFederationDisallowed\":false,\"isRemoteConnectSupported\":false,\"federationFlags\":0,\"isSignup\":false,\"flowToken\":\"" + authStuff.sFT + "\",\"isAccessPassSupported\":true}";

        temp_url = "https://login.microsoftonline.com/common/GetCredentialType?mkt=en-US";
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(temp_url),
            Method = HttpMethod.Post,
            Headers =
            {
                {"hpgrequestid", authStuff.sessionId },
                {"canary", authStuff.apiCanary},
                {"hpgid", authStuff.hpgid.ToString()},
                {"hpgact", authStuff.hpgact.ToString()},
                {"client-request-id", authStuff.correlationId}
            },
            Content = new StringContent(content)
        };
        temp_res = await _httpClient.SendAsync(request);

        //temp_res = await _httpClient.PostAsync(temp_url, new StringContent(content));
        var temp = await temp_res.Content.ReadAsStringAsync();
        var  credType = JsonSerializer.Deserialize<CredentialType>(temp);


        temp_url = credType.Credentials.FederationRedirectUrl;
        temp_url = temp_url.Replace("wctx=", "wctx=LoginOptions%3D3%26") + "&cbcxt=";
        temp_res = await _httpClient.GetAsync(temp_url);
        temp_content = await temp_res.Content.ReadAsStringAsync();

        var samlRequest = RegExp("\"SAMLRequest\\\" value=\\\"([^\\\"]*)\"", temp_content);
        var relayState = RegExp("\"RelayState\\\" value=\\\"([^\\\"]*)\"", temp_content);
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);

        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new []
        {
            new KeyValuePair<string, string>("SAMLRequest", samlRequest),
            new KeyValuePair<string, string>("RelayState", relayState)
        }));

      
        
        temp_url = temp_res.Headers.Location?.ToString();
        var samlTransactionId = new Uri(temp_url).Query.Split("&").First(x => x.StartsWith("SAMLTRANSACTIONID=")).Split("=")[1];
        temp_res = await _httpClient.GetAsync(temp_url);



        temp_url = temp_res.Headers.Location?.ToString();
        temp_res = await _httpClient.GetAsync(temp_url);

        //TODO: Parametern startpage är Base64encodad och innehåller SAMLTRANSACTIONID. Måste göras om där den finns.
        
        // Byt till Elevinloggning. Behövs?
        temp_url = $"https://login001.stockholm.se/siteminderagent/forms/aelever.jsp?SMAUTHREASON=0&SMAGENTNAME=IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1&SMQUERYDATA=null&TARGET=https://login001.stockholm.se/affwebservices/redirectjsp/eduadfs.jsp?SMPORTALURL=https%3A%2F%2Flogin001.stockholm.se%2Faffwebservices%2Fpublic%2Fsaml2sso&SAMLTRANSACTIONID={samlTransactionId}";
        temp_res = await _httpClient.GetAsync(temp_url);
        

        //Ladda sida för inloggning med användnamn och lösen
        temp_url = UpdateSamlTransactionIdInEncodedStartpageParam($"https://login001.stockholm.se/siteminderagent/forms/loginForm.jsp?SMAGENTNAME=IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1&POSTTARGET=https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9sb2dpbjAwMS5zdG9ja2hvbG0uc2UvYWZmd2Vic2VydmljZXMvcmVkaXJlY3Rqc3AvZWR1YWRmcy5qc3A/U01QT1JUQUxVUkw9aHR0cHMlM0ElMkYlMkZsb2dpbjAwMS5zdG9ja2hvbG0uc2UlMkZhZmZ3ZWJzZXJ2aWNlcyUyRnB1YmxpYyUyRnNhbWwyc3NvJlNBTUxUUkFOU0FDVElPTklEPTEwMjkwNThjLTM0MTg2YWFhLTRmYWRiYzY5LTRkN2RkMDU3LTczYmRkNGQ3LTA1MA==&TARGET=https://login001.stockholm.se/affwebservices/redirectjsp/eduadfs.jsp?SMPORTALURL=https%3A%2F%2Flogin001.stockholm.se%2Faffwebservices%2Fpublic%2Fsaml2sso&SAMLTRANSACTIONID={samlTransactionId}", samlTransactionId);
        temp_res = await _httpClient.GetAsync(temp_url);
        temp_content = await temp_res.Content.ReadAsStringAsync();
        var tttttt = RegExp("name=target value='([^']*)'", temp_content);

        // Här skickas inloggningsuppgifterna in
        temp_url = "https://login001.stockholm.se/siteminderagent/forms/login.fcc";
        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("user", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("SMENC", "ISO-8859-1"),
            new KeyValuePair<string, string>("SMLOCALE", "US-EN"),
            new KeyValuePair<string, string>("target", UpdateSamlTransactionIdInEncodedStartpageParam("https://login001.stockholm.se/NECSelev/form/b64startpage.jsp?startpage=aHR0cHM6Ly9sb2dpbjAwMS5zdG9ja2hvbG0uc2UvYWZmd2Vic2VydmljZXMvcmVkaXJlY3Rqc3AvZWR1YWRmcy5qc3A/U01QT1JUQUxVUkw9aHR0cHMlM0ElMkYlMkZsb2dpbjAwMS5zdG9ja2hvbG0uc2UlMkZhZmZ3ZWJzZXJ2aWNlcyUyRnB1YmxpYyUyRnNhbWwyc3NvJlNBTUxUUkFOU0FDVElPTklEPTEwMjkwNThjLTM0MTg2YWFhLTRmYWRiYzY5LTRkN2RkMDU3LTczYmRkNGQ3LTA1MA==", samlTransactionId)),
            new KeyValuePair<string, string>("smauthreason", "null"),
            new KeyValuePair<string, string>("smagentname", "IfNE0iMOtzq2TcxFADHylR6rkmFtwzoxRKh5nRMO9NBqIxHrc38jFyt56FASdxk1"),
            new KeyValuePair<string, string>("smquerydata", "null"),
            new KeyValuePair<string, string>("postpreservationdata", "null"),
            new KeyValuePair<string, string>("submit", "")
        }));


        while (temp_res.Headers.Location != null)
        {
            temp_url = temp_res.Headers.Location?.ToString();
            temp_res = await _httpClient.GetAsync(temp_url);
        }
        
        temp_content = await temp_res.Content.ReadAsStringAsync();
        var samlResponse = RegExp("\"SAMLResponse\\\" value=\\\"([^\\\"]*)\"", temp_content);
        relayState = RegExp("\"RelayState\\\" value=\\\"([^\\\"]*)\"", temp_content);

        // Har hämtat rad 72 och hämtat variablerna

        temp_url = "https://fs.edu.stockholm.se/adfs/ls/";
        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("SAMLResponse", samlResponse),
            new KeyValuePair<string, string>("RelayState", relayState)
        }));
        temp_content = await temp_res.Content.ReadAsStringAsync();

        var wa = RegExp("\"wa\\\" value=\\\"([^\\\"]*)\"", temp_content);
        var wresult = HttpUtility.HtmlDecode(RegExp("\"wresult\\\" value=\\\"([^\\\"]*)\"", temp_content));
        var wctx = HttpUtility.HtmlDecode(RegExp("\"wctx\\\" value=\\\"([^\\\"]*)\"", temp_content));

        // https://login.microsoftonline.com/login.srf
        _cookieContainer.Add(new Cookie("ESTSWCTXFLOWTOKEN", credType.FlowToken, "/", "login.microsoftonline.com"));
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);
        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("wa", wa),
            new KeyValuePair<string, string>("wresult", wresult),
            new KeyValuePair<string, string>("wctx", wctx)
        }));

        // Skickat data till login.srf på rad 76


        temp_content = await temp_res.Content.ReadAsStringAsync();
        json = RegExp("\\$Config=(.*});", temp_content);
        var loginSrfAnswer = JsonSerializer.Deserialize<LoginSrfAnswer>(json, jsonSerializerOptions);


        _cookieContainer.Add(new Cookie("ESTSWCTXFLOWTOKEN", null, "/", "login.microsoftonline.com"));


        //_cookieContainer.Add(new Cookie("AADSSO", "NA|NoExtension", "/", "login.microsoftonline.com"));
        //_cookieContainer.Add(new Cookie("brcap", "0", "/", "login.microsoftonline.com"));
        //_cookieContainer.Add(new Cookie("clrc", null, "/", "login.microsoftonline.com"));
        //_cookieContainer.Add(new Cookie("ESTSAUTHPERSISTENT", null, "/", "login.microsoftonline.com"));
        //_cookieContainer.Add(new Cookie("ESTSAUTH", null, "/", "login.microsoftonline.com"));
        //_cookieContainer.Add(new Cookie("ESTSAUTHLIGHT", null, "/", "login.microsoftonline.com"));
        
        //_cookieContainer.Add(new Cookie("ch", null, "/", "login.microsoftonline.com"));
        //_cookieContainer.Add(new Cookie("ESTSSC", "00", "/", "login.microsoftonline.com"));



        var ccccc = _cookieContainer.GetAllCookies();
        // We are here. 
        //TODO: previous page (login.srf) loads some javascript files that populates the parameters for next call

        temp_url = "https://login.microsoftonline.com/kmsi";
        temp_res = await _httpClient.PostAsync(temp_url, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("LoginOptions", "3"),
            new KeyValuePair<string, string>("type", "28"),
            new KeyValuePair<string, string>("ctx", loginSrfAnswer.sCtx),
            new KeyValuePair<string, string>("hpgrequestid", loginSrfAnswer.sessionId),
            new KeyValuePair<string, string>("flowToken", loginSrfAnswer.sFT),
            new KeyValuePair<string, string>("canary", loginSrfAnswer.canary),
            new KeyValuePair<string, string>("i19", "2340"),
        }));

        temp_content = await temp_res.Content.ReadAsStringAsync();

        var code = RegExp("\"code\\\" value=\\\"([^\\\"]*)\"", temp_content);
        var id_token = HttpUtility.HtmlDecode(RegExp("\"id_token\\\" value=\\\"([^\\\"]*)\"", temp_content));
        var state = RegExp("\"state\\\" value=\\\"([^\\\"]*)\"", temp_content);
        
        var sessionState = RegExp("\"session_state\\\" value=\\\"([^\\\"]*)\"", temp_content);
        var correlationId = RegExp("\"correlation_id\\\" value=\\\"([^\\\"]*)\"", temp_content);
        temp_url = RegExp("action=\\\"([^\\\"]*)", temp_content);


        temp_url = "";
        temp_res = await _httpClient.GetAsync(temp_url);

    }

    private string RegExp(string pattern, string source)
    {
        var reg = new Regex(pattern);
        var matches = reg.Matches(source);
        
        return matches[0].Groups[1].Value;
    }

    private string UpdateSamlTransactionIdInEncodedStartpageParam(string url, string samlTransationId)
    {
       // var startpage = new Uri(url).Query.Split("&").First(x => x.StartsWith("startpage=")).Split("=")[1];
       // var startpage = url.Substring(url.IndexOf('?')+1).Split("&").First(x => x.StartsWith("startpage=")).Split("=")[1];
       var startpage = RegExp("startpage=([^&]*)", url);
        var decodedStartpage = Encoding.UTF8.GetString(Convert.FromBase64String(startpage));
        var oldSamlTransactionId = new Uri(decodedStartpage).Query.Split("&").First(x => x.StartsWith("SAMLTRANSACTIONID=")).Split("=")[1];
        var newStartpage = decodedStartpage.Replace(oldSamlTransactionId, samlTransationId);
        var newEncodedStartpage = Convert.ToBase64String(Encoding.UTF8.GetBytes(newStartpage));

        var newUrl = url.Replace(startpage, newEncodedStartpage);
        return newUrl;
    }
}

class AuthorizeStuff
{
    public string sFT { get; set; }
    public string sCtx { get; set; }
    public string apiCanary { get; set; }
    public string correlationId { get; set; }
    public int hpgact { get; set; }
    public int hpgid { get; set; }
    public string sessionId { get; set; }
}

// Autogenerated from answer to https://login.microsoftonline.com/common/GetCredentialType?mkt=en-US
// CredentialType myDeserializedClass = JsonConvert.DeserializeObject<CredentialType>(myJsonResponse);
public class Credentials
{
    public int PrefCredential { get; set; }
    public bool HasPassword { get; set; }
    public object RemoteNgcParams { get; set; }
    public object FidoParams { get; set; }
    public object SasParams { get; set; }
    public object CertAuthParams { get; set; }
    public object GoogleParams { get; set; }
    public object FacebookParams { get; set; }
    public string FederationRedirectUrl { get; set; }
}

public class EstsProperties
{
    public object UserTenantBranding { get; set; }
    public int DomainType { get; set; }
}

public class CredentialType
{
    public string Username { get; set; }
    public string Display { get; set; }
    public int IfExistsResult { get; set; }
    public bool IsUnmanaged { get; set; }
    public int ThrottleStatus { get; set; }
    public Credentials Credentials { get; set; }
    public EstsProperties EstsProperties { get; set; }
    public string FlowToken { get; set; }
    public bool IsSignupDisallowed { get; set; }
    public string apiCanary { get; set; }
}

// ---- login.srf

public class AppInsightsConfig
{
    public string instrumentationKey { get; set; }
    public WebAnalyticsConfiguration webAnalyticsConfiguration { get; set; }
}

public class AutoCapture
{
    public bool jsError { get; set; }
}

public class B
{
    public string name { get; set; }
    public int major { get; set; }
    public int minor { get; set; }
}

public class Browser
{
    public int ltr { get; set; }
    public int Chrome { get; set; }
    public int _Win { get; set; }
    public int _M101 { get; set; }
    public int _D0 { get; set; }
    public int Full { get; set; }
    public int Win81 { get; set; }
    public int RE_WebKit { get; set; }
    public B b { get; set; }
    public Os os { get; set; }
    public string V { get; set; }
}

public class ClientEvents
{
    public bool enabled { get; set; }
    public bool telemetryEnabled { get; set; }
    public bool useOneDSEventApi { get; set; }
    public int flush { get; set; }
    public bool autoPost { get; set; }
    public int autoPostDelay { get; set; }
    public int minEvents { get; set; }
    public int maxEvents { get; set; }
    public int pltDelay { get; set; }
    public AppInsightsConfig appInsightsConfig { get; set; }
    public string defaultEventName { get; set; }
    public int serviceID { get; set; }
}

public class ClientMetricsModes
{
    public int None { get; set; }
    public int SubmitOnPost { get; set; }
    public int SubmitOnRedirect { get; set; }
    public int InstrumentPlt { get; set; }
}

public class Desktopsso
{
    public string authenticatingmessage { get; set; }
}

public class DesktopSsoConfig
{
    public bool isEdgeAnaheimAllowed { get; set; }
    public string iwaEndpointUrlFormat { get; set; }
    public string iwaSsoProbeUrlFormat { get; set; }
    public string iwaIFrameUrlFormat { get; set; }
    public int iwaRequestTimeoutInMs { get; set; }
    public bool startDesktopSsoOnPageLoad { get; set; }
    public int progressAnimationTimeout { get; set; }
    public bool isEdgeAllowed { get; set; }
    public string minDssoEdgeVersion { get; set; }
    public bool isSafariAllowed { get; set; }
    public string redirectUri { get; set; }
    public RedirectDssoErrorPostParams redirectDssoErrorPostParams { get; set; }
    public bool isIEAllowedForSsoProbe { get; set; }
    public string edgeRedirectUri { get; set; }
}

public class Enums
{
    public ClientMetricsModes ClientMetricsModes { get; set; }
}

public class Instr
{
    public string pageload { get; set; }
    public string dssostatus { get; set; }
}

public class Loader
{
    public List<string> cdnRoots { get; set; }
    public bool logByThrowing { get; set; }
}

public class Locale
{
    public string mkt { get; set; }
    public int lcid { get; set; }
}

public class OAppCobranding
{
}

public class Os
{
    public string name { get; set; }
    public string version { get; set; }
}

public class RedirectDssoErrorPostParams
{
    public string error { get; set; }
    public string error_description { get; set; }
    public string state { get; set; }
}

public class LoginSrfAnswer
{
    public int iMaxStackForKnockoutAsyncComponents { get; set; }
    public bool fShowButtons { get; set; }
    public string urlCdn { get; set; }
    public string urlDefaultFavicon { get; set; }
    public string urlFooterTOU { get; set; }
    public string urlFooterPrivacy { get; set; }
    public string urlPost { get; set; }
    public int iPawnIcon { get; set; }
    public string sPOST_Username { get; set; }
    public string sFT { get; set; }
    public string sFTName { get; set; }
    public string sCtx { get; set; }
    public string sCanaryTokenName { get; set; }
    public bool fEnableOneDSClientTelemetry { get; set; }
    public string urlReportPageLoad { get; set; }
    public object dynamicTenantBranding { get; set; }
    public object staticTenantBranding { get; set; }
    public OAppCobranding oAppCobranding { get; set; }
    public int iBackgroundImage { get; set; }
    public bool fApplicationInsightsEnabled { get; set; }
    public int iApplicationInsightsEnabledPercentage { get; set; }
    public string urlSetDebugMode { get; set; }
    public bool fEnableCssAnimation { get; set; }
    public bool fAllowGrayOutLightBox { get; set; }
    public bool fIsRemoteNGCSupported { get; set; }
    public DesktopSsoConfig desktopSsoConfig { get; set; }
    public int iSessionPullType { get; set; }
    public bool fUseSameSite { get; set; }
    public int uiflavor { get; set; }
    public bool fOfflineAccountVisible { get; set; }
    public bool fEnableUserStateFix { get; set; }
    public bool fShowAccessPassPeek { get; set; }
    public bool fUpdateSessionPollingLogic { get; set; }
    public bool fFloatTileMenu { get; set; }
    public int scid { get; set; }
    public int hpgact { get; set; }
    public int hpgid { get; set; }
    public string pgid { get; set; }
    public string apiCanary { get; set; }
    public string canary { get; set; }
    public string correlationId { get; set; }
    public string sessionId { get; set; }
    public Locale locale { get; set; }
    public int slMaxRetry { get; set; }
    public bool slReportFailure { get; set; }
    public Strings strings { get; set; }
    public Enums enums { get; set; }
    public Urls urls { get; set; }
    public Browser browser { get; set; }
    public Watson watson { get; set; }
    public Loader loader { get; set; }
    public ServerDetails serverDetails { get; set; }
    public ClientEvents clientEvents { get; set; }
    public bool fApplyAsciiRegexOnInput { get; set; }
    public string country { get; set; }
    public bool fBreakBrandingSigninString { get; set; }
    public string urlNoCookies { get; set; }
    public bool fTrimChromeBssoUrl { get; set; }
    public int inlineMode { get; set; }
    public bool fShowCopyDebugDetailsLink { get; set; }
}

public class ServerDetails
{
    public string slc { get; set; }
    public string dc { get; set; }
    public string ri { get; set; }
    public Ver ver { get; set; }
    public DateTime rt { get; set; }
    public int et { get; set; }
}

public class Strings
{
    public Desktopsso desktopsso { get; set; }
}

public class Urls
{
    public Instr instr { get; set; }
}

public class Ver
{
    public List<int> v { get; set; }
}

public class Watson
{
    public string url { get; set; }
    public string bundle { get; set; }
    public string sbundle { get; set; }
    public string fbundle { get; set; }
    public int resetErrorPeriod { get; set; }
    public int maxCorsErrors { get; set; }
    public int maxInjectErrors { get; set; }
    public int maxErrors { get; set; }
    public int maxTotalErrors { get; set; }
    public List<string> expSrcs { get; set; }
    public bool envErrorRedirect { get; set; }
    public string envErrorUrl { get; set; }
}

public class WebAnalyticsConfiguration
{
    public AutoCapture autoCapture { get; set; }
}

