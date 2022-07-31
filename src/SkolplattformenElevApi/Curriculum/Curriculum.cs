using SkolplattformenElevApi.FakeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Curriculum
{

    internal class Curriculum
    {
        private readonly CurriculumData _data;
        public Curriculum()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("Curriculum.sv.json"));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();

                _data = JsonSerializer.Deserialize<CurriculumData>(result)!;
            }
        }

        public string GetSubject(string lessonCode)
        {

         


            throw new NotImplementedException();
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    internal class Categories
    {
        public string trainingSchool { get; set; }
        public string modernLanguages { get; set; }
        public string modernLanguagesA1 { get; set; }
        public string modernLanguagesA2 { get; set; }
        public string modernLanguagesAlt { get; set; }
        public string motherTounge { get; set; }
        public string unknown { get; set; }
        public string misc { get; set; }
    }

    internal class Languages
    {
        public string ACE { get; set; }
        public string ACH { get; set; }
        public string AAR { get; set; }
        public string AFR { get; set; }
        public string AKA { get; set; }
        public string SQI { get; set; }
        public string AMH { get; set; }
        public string ARA { get; set; }
        public string HYE { get; set; }
        public string AII { get; set; }
        public string AYM { get; set; }
        public string AZE { get; set; }
        public string BAL { get; set; }
        public string BAM { get; set; }
        public string BAI { get; set; }
        public string EUS { get; set; }
        public string BEM { get; set; }
        public string BEN { get; set; }
        public string BER { get; set; }
        public string BIL { get; set; }
        public string BYN { get; set; }
        public string BOS { get; set; }
        public string BUL { get; set; }
        public string MYA { get; set; }
        public string CEB { get; set; }
        public string DAN { get; set; }
        public string DAR { get; set; }
        public string PRS { get; set; }
        public string DMQ { get; set; }
        public string DIV { get; set; }
        public string ENG { get; set; }
        public string EST { get; set; }
        public string EWE { get; set; }
        public string FIJ { get; set; }
        public string FIN { get; set; }
        public string VLS { get; set; }
        public string FRA { get; set; }
        public string FAO { get; set; }
        public string GAA { get; set; }
        public string KAT { get; set; }
        public string GRE { get; set; }
        public string KAL { get; set; }
        public string GUJ { get; set; }
        public string HEB { get; set; }
        public string HIN { get; set; }
        public string IBO { get; set; }
        public string IND { get; set; }
        public string ISL { get; set; }
        public string ITA { get; set; }
        public string JPN { get; set; }
        public string YID { get; set; }
        public string KAM { get; set; }
        public string KHM { get; set; }
        public string KAN { get; set; }
        public string KAR { get; set; }
        public string CAT { get; set; }
        public string KAZ { get; set; }
        public string KIK { get; set; }
        public string ZHO { get; set; }
        public string CMN { get; set; }
        public string HAK { get; set; }
        public string YUE { get; set; }
        public string NAN { get; set; }
        public string KIN { get; set; }
        public string KIR { get; set; }
        public string RUN { get; set; }
        public string KON { get; set; }
        public string KOR { get; set; }
        public string ROP { get; set; }
        public string HRV { get; set; }
        public string KRO { get; set; }
        public string KUR { get; set; }
        public string CKB { get; set; }
        public string KMR { get; set; }
        public string SDH { get; set; }
        public string LAO { get; set; }
        public string LAV { get; set; }
        public string LMA { get; set; }
        public string LIN { get; set; }
        public string LIT { get; set; }
        public string LUG { get; set; }
        public string LUO { get; set; }
        public string MKD { get; set; }
        public string MLG { get; set; }
        public string MSA { get; set; }
        public string MAL { get; set; }
        public string MLT { get; set; }
        public string MNK { get; set; }
        public string MRI { get; set; }
        public string MAR { get; set; }
        public string MYX { get; set; }
        public string FIT { get; set; }
        public string MON { get; set; }
        public string NLD { get; set; }
        public string NEP { get; set; }
        public string NOR { get; set; }
        public string NYA { get; set; }
        public string ORM { get; set; }
        public string PUS { get; set; }
        public string PTN { get; set; }
        public string FAS { get; set; }
        public string POL { get; set; }
        public string POR { get; set; }
        public string PAN { get; set; }
        public string ROM { get; set; }
        public string RMC { get; set; }
        public string RML { get; set; }
        public string RMN { get; set; }
        public string RMF { get; set; }
        public string RMO { get; set; }
        public string RMU { get; set; }
        public string RMY { get; set; }
        public string RON { get; set; }
        public string RUS { get; set; }
        public string SSY { get; set; }
        public string NSM { get; set; }
        public string SMI { get; set; }
        public string SMJ { get; set; }
        public string SJE { get; set; }
        public string SMA { get; set; }
        public string SJU { get; set; }
        public string SMO { get; set; }
        public string SRP { get; set; }
        public string HBS { get; set; }
        public string SOT { get; set; }
        public string SNA { get; set; }
        public string SIN { get; set; }
        public string SLK { get; set; }
        public string SLV { get; set; }
        public string SOM { get; set; }
        public string SPA { get; set; }
        public string SWA { get; set; }
        public string SYC { get; set; }
        public string SYR { get; set; }
        public string TRU { get; set; }
        public string TLG { get; set; }
        public string TAM { get; set; }
        public string TAT { get; set; }
        public string TEL { get; set; }
        public string THA { get; set; }
        public string TIB { get; set; }
        public string TIG { get; set; }
        public string TIR { get; set; }
        public string CES { get; set; }
        public string TON { get; set; }
        public string TSN { get; set; }
        public string TUR { get; set; }
        public string DEU { get; set; }
        public string UIG { get; set; }
        public string UKR { get; set; }
        public string HUN { get; set; }
        public string URD { get; set; }
        public string UZB { get; set; }
        public string VIE { get; set; }
        public string WOL { get; set; }
        public string YOR { get; set; }
        public string ZUL { get; set; }
        public string SPK { get; set; }
    }

    internal class Misc
    {
        public string LUNCH { get; set; }
        public string PRANDIUM { get; set; }
        public string MTID { get; set; }
        public string RAST { get; set; }
    }

    internal class CurriculumData
    {
        public Subjects subjects { get; set; }
        public TraningsskolaSubjects traningsskolaSubjects { get; set; }
        public SpecialLanguages specialLanguages { get; set; }
        public Languages languages { get; set; }
        public Categories categories { get; set; }
        public Misc misc { get; set; }
    }

    internal class SpecialLanguages
    {
        public string EN { get; set; }
        public string FR { get; set; }
        public string FI { get; set; }
        public string IT { get; set; }
        public string JAP { get; set; }
        public string KI { get; set; }
        public string PO { get; set; }
        public string RY { get; set; }
        public string SAM { get; set; }
        public string SP { get; set; }
        public string SV { get; set; }
        public string SVA { get; set; }
        public string TN { get; set; }
        public string TY { get; set; }
    }

    internal class Subjects
    {
        public string BL { get; set; }
        public string EN { get; set; }
        public string HKK { get; set; }
        public string IDH { get; set; }
        public string MA { get; set; }
        public string MU { get; set; }
        public string NO { get; set; }
        public string BI { get; set; }
        public string FY { get; set; }
        public string KE { get; set; }
        public string SO { get; set; }
        public string GE { get; set; }
        public string HI { get; set; }
        public string RE { get; set; }
        public string SH { get; set; }
        public string SL { get; set; }
        public string SV { get; set; }
        public string SVA { get; set; }
        public string TN { get; set; }
        public string TK { get; set; }
        public string DA { get; set; }
        public string JU { get; set; }
        public string ES { get; set; }
    }

    internal class TraningsskolaSubjects
    {
        public string KOM { get; set; }
        public string MOT { get; set; }
        public string VAA { get; set; }
        public string VEU { get; set; }
    }


}
