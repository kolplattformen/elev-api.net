namespace SkolplattformenElevApi.Models.Timetable;
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
//public class BoxList
//{
//    public int X { get; set; }
//    public int Y { get; set; }
//    public int Width { get; set; }
//    public int Height { get; set; }
//    public string BColor { get; set; }
//    public string FColor { get; set; }
//    public int Id { get; set; }
//    public int? ParentId { get; set; }
//    public string Type { get; set; }
//    public List<string> LessonGuids { get; set; }
//}

public class TimetableRenderData
{
//    public List<TextList> TextList { get; set; }
//    public List<BoxList> BoxList { get; set; }
//    public List<LineList> LineList { get; set; }
    public List<LessonInfo> LessonInfo { get; set; }
}

public class LessonInfo
{
    public string GuidId { get; set; }
    public string[] Texts { get; set; }
    public string TimeStart { get; set; }
    public string TimeEnd { get; set; }
    public int DayOfWeekNumber { get; set; }
    public string BlockName { get; set; }
}

//public class LineList
//{
//    public int P1x { get; set; }
//    public int P1y { get; set; }
//    public int P2x { get; set; }
//    public int P2y { get; set; }
//    public string Color { get; set; }
//    public int Id { get; set; }
//    public int ParentId { get; set; }
//    public string Type { get; set; }
//}

public class TimetableRenderResponse
{
    public object Error { get; set; }
    public TimetableRenderData Data { get; set; }
    public object Exception { get; set; }
    public List<object> Validation { get; set; }
    public DateTime SessionExpires { get; set; }
    public bool NeedSessionRefresh { get; set; }
}

//public class TextList
//{
//    public int X { get; set; }
//    public int Y { get; set; }
//    public string FColor { get; set; }
//    public double Fontsize { get; set; }
//    public string Text { get; set; }
//    public bool Bold { get; set; }
//    public bool Italic { get; set; }
//    public int Id { get; set; }
//    public int ParentId { get; set; }
//    public string Type { get; set; }
//}

