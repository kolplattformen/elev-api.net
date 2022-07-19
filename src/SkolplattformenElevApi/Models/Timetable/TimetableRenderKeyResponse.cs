using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models.Timetable;
public class TimetableRenderKeyData
{
    public string Key { get; set; }
}

public class TimetableRenderKeyResponse
{
    public object Error { get; set; }
    public TimetableRenderKeyData Data { get; set; }
    public object Exception { get; set; }
    public List<object> Validation { get; set; }
    public DateTime SessionExpires { get; set; }
    public bool NeedSessionRefresh { get; set; }
}