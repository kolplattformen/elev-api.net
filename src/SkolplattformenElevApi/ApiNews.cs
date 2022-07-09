using System.Net.Http.Headers;

namespace SkolplattformenElevApi
{
    public partial class Api
    {
        public async Task<string> GetNewsItemList()
        {
            var query = "{ \"request\": {\"Querytext\":\"\",\"QueryTemplate\":\"{searchterms} -SiteTitle:\\\"Användarstöd\\\" AND (LastModifiedTime=\\\"this year\\\" OR LastModifiedTime=\\\"last year\\\") AND  ((ContentTypeId:0x0101009D1CB255DA76424F860D91F20E6C4118* AND PromotedState=2 AND NOT ContentTypeId:0x0101009D1CB255DA76424F860D91F20E6C4118002A50BFCFB7614729B56886FADA02339B00873E381CC9DD4F2E808A377A72C311BB*))\",\"ClientType\":\"HighlightedContentWebPart\",\"RowLimit\":6,\"RowsPerPage\":6,\"TimeZoneId\":4,\"SelectProperties\":[\"ContentType\",\"ContentTypeId\",\"Title\",\"EditorOwsUser\",\"ModifiedBy\",\"LastModifiedBy\",\"FileExtension\",\"FileType\",\"Path\",\"SiteName\",\"SiteTitle\",\"PictureThumbnailURL\",\"DefaultEncodingURL\",\"LastModifiedTime\",\"ListID\",\"ListItemID\",\"SiteID\",\"WebId\",\"UniqueID\",\"LastModifiedTime\",\"SitePath\",\"UserName\",\"ProfileImageSrc\",\"Name\",\"Initials\",\"WebPath\",\"PreviewUrl\",\"IconUrl\",\"AccentColor\",\"CardType\",\"TipActionLabel\",\"TipActionButtonIcon\",\"ClassName\",\"TelemetryProperties\",\"ImageOverlapText\",\"ImageOverlapTextAriaLabel\",\"SPWebUrl\",\"IsExternalContent\",\"MediaServiceMetadata\",\"LastModifiedTimeForRetention\"],\"Properties\":[{\"Name\":\"TrimSelectProperties\",\"Value\":{\"StrVal\":\"1\",\"QueryPropertyValueTypeIndex\":1}},{\"Name\":\"EnableDynamicGroups\",\"Value\":{\"BoolVal\":\"True\",\"QueryPropertyValueTypeIndex\":3}},{\"Name\":\"EnableMultiGeoSearch\",\"Value\":{\"BoolVal\":\"False\",\"QueryPropertyValueTypeIndex\":3}}],\"SortList\":[{\"Property\":\"LastModifiedTime\",\"Direction\":1}],\"SourceId\":\"8413CD39-2156-4E00-B54D-11EFD9ABDB89\",\"TrimDuplicates\":false} }";
            var temp_url = "https://elevstockholm.sharepoint.com/sites/skolplattformen/_api/search/postquery";
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(temp_url),
                Method = HttpMethod.Post,
                Headers =
                {
                    { "odata-version", "3.0" },
                    { "originalcorrelationid", _sharePointRequestGuid },
                    { "Referer", "https://elevstockholm.sharepoint.com/sites/skolplattformen/" },
                    { "SdkVersion", "SPFx/ContentRollupWebPart/daf0b71c-6de8-4ef7-b511-faae7c388708" },
                    { "x-requestdigest", _formDigestValue },
                    { "Origin", "https://elevstockholm.sharepoint.com" },
                },
                Content = new StringContent(query)
            };
            request.Headers.TryAddWithoutValidation("accept", new[] { "application/json;odata=nometadata" });
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=utf-8");

            var temp_res = await _httpClient.SendAsync(request);
            var temp_content = await temp_res.Content.ReadAsStringAsync();

            return temp_content;
        }

    }
}
