using SkolplattformenElevApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Utils;

internal static class Enrichers
{
    internal static void EnrichTimetableWithCurriculum(List<TimeTableLesson> timetable)
    {
        var curriculum = new Curriculum.Curriculum();

        foreach (var l in timetable)
        {
            var subject = curriculum.GetSubject(l.LessonCode);
            if (subject != null)
            {
                l.LessonName = subject.Name;
            }

        }
    }

    internal static void EnrichTimetableWithTeachers(List<TimeTableLesson> timetable, List<Teacher> teachers)
    {
        foreach (var l in timetable)
        {
            var teacher = teachers.FirstOrDefault(t =>
                $"{t.Firstname.Substring(0, 1).ToUpper()}{t.Lastname.Substring(0, 2).ToUpper()}" == l.TeacherCode);

            if (teacher != null)
            {
                l.TeacherName = $"{teacher.Firstname} {teacher.Lastname}";
            }
        }
    }
}
   
    

