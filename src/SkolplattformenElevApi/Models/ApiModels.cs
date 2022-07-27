using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models
{
    

    public class ApiUser
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
      //  public string Firstname { get; set; }
        //public string Lastname { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PrimarySchoolName { get; set; } = string.Empty;
        public Guid PrimarySchoolGuid { get; set; }
        public List<ApiSchool> Schools { get; set; } = new List<ApiSchool>();
    }

    public class ApiSchool
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        
        
    }

    public class ApiSchoolDetails
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
       
        public string PostalCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        // public string Locality { get; set; }
        public string VisitingAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PrincipalName { get; set; } = string.Empty;
        public string PrincipalPhone { get; set; } = string.Empty;
        public string PrincipalEmail { get; set; } = string.Empty;  
    }

    public class ApiTeacher
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
      //  public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class ApiCalendarItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsAllDay { get; set; } // start.minutes.utc == 00 and end-start == 24 hours ?
        public string WebLink { get; set; }
    }

    public class ApiPlannedAbsenceItem
    {
        public DateTime Created { get; set; }
        public string AbsenceId { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public bool IsFullDayAbsence { get; set; }
        public string ReasonDescription { get; set; }
        public string Reporter { get; set; }
    }

    // TODO: News items

    public class ApiTimeTableLesson
    {
        public int DayOfWeekNumber { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string LessonCode { get; set; }
        public string LessonName { get; set; }
        public string TeacherCode { get; set; }
        public string? TeacherName { get; set; }
        public string Location { get; set; }
    }
}
