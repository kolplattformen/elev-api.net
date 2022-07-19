namespace SkolplattformenElevApi.Models.Absence;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class CurrentUserGuidData
{
    public string CurrentUserGuid { get; set; }
}

public class CurrentUserGuidResponse
{
    public object Error { get; set; }
    public CurrentUserGuidData Data { get; set; }
    public object Exception { get; set; }
    public List<object> Validation { get; set; }
    public DateTime SessionExpires { get; set; }
    public bool NeedSessionRefresh { get; set; }
}
