using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models.Sharepoint
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class CustomMetadata
    {
        public ImageSource imageSource { get; set; }
    }

    public class HtmlStrings
    {
    }

    public class ImageSource
    {
        public string siteid { get; set; }
        public string webid { get; set; }
        public string listid { get; set; }
        public string uniqueid { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }

    public class ImageSources
    {
        public string imageSource { get; set; }
    }

    public class Links
    {
    }

    public class PageSettingsSlice
    {
        public bool isDefaultDescription { get; set; }
        public bool isDefaultThumbnail { get; set; }
        public bool isSpellCheckEnabled { get; set; }
    }

    public class Position
    {
        public int controlIndex { get; set; }
        public int sectionIndex { get; set; }
        public int sectionFactor { get; set; }
        public int zoneIndex { get; set; }
        public int layoutIndex { get; set; }
    }

    public class Properties
    {
        public int imageSourceType { get; set; }
        public string altText { get; set; }
        public string linkUrl { get; set; }
        public string overlayText { get; set; }
        public string fileName { get; set; }
        public string siteId { get; set; }
        public string webId { get; set; }
        public string listId { get; set; }
        public string uniqueId { get; set; }
        public int imgWidth { get; set; }
        public int imgHeight { get; set; }
        public string alignment { get; set; }
        public bool fixAspectRatio { get; set; }
    }

    public class CanvasContent
    {
        public int controlType { get; set; }
        public string id { get; set; }
        public Position position { get; set; }
        public bool addedFromPersistedData { get; set; }
        public string innerHTML { get; set; }
        public string webPartId { get; set; }
        public int? reservedHeight { get; set; }
        public int? reservedWidth { get; set; }
        public WebPartData webPartData { get; set; }
        public PageSettingsSlice pageSettingsSlice { get; set; }
    }

    public class SearchablePlainTexts
    {
        public string captionText { get; set; }
    }

    public class ServerProcessedContent
    {
        public HtmlStrings htmlStrings { get; set; }
        public SearchablePlainTexts searchablePlainTexts { get; set; }
        public ImageSources imageSources { get; set; }
        public Links links { get; set; }
        public CustomMetadata customMetadata { get; set; }
    }

    public class WebPartData
    {
        public string id { get; set; }
        public string instanceId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<object> audiences { get; set; }
        public ServerProcessedContent serverProcessedContent { get; set; }
        public string dataVersion { get; set; }
        public Properties properties { get; set; }
    }


}
