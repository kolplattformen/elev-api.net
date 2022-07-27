namespace SkolplattformenElevApi.Models.Internal.StockholmAzureApi
{
    public class SchoolDetails
    {
        public string ExternalId { get; set; }
        public string SchoolName { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string VisitingAddress { get; set; }
        public string Email { get; set; }
        public SchoolPrincipal Principal { get; set; }
    }

    public class SchoolPrincipal
    {
        public string Fullname { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }

    public class GetSchoolResponse
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public SchoolDetails Data { get; set; }
    }

}
