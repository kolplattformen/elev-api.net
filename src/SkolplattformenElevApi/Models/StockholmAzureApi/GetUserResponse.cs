using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Models.StockholmAzureApi
{
    public class UserData
    {
        public string Id { get; set; }
        public string PrimarySchool { get; set; }
        public string Name { get; set; }
        public string SchoolType { get; set; }
        public string UserType { get; set; }
        public string ExternalId { get; set; }
        public string UPN { get; set; }
        public List<School> Schools { get; set; }
    }

    public class GetUserResponse
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public UserData Data { get; set; }
    }

    public class School
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string ExternalId { get; set; }
        public string Url { get; set; }
        public object MergeSchoolName { get; set; }
        public object MergeSchoolExternalId { get; set; }
    }

}
