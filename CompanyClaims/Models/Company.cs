using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyClaimsApi.Models
{
    public class Company
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public DateTime InsuranceEndDate { get; set; }
        public bool ActiveInsurancePolicy =>  Convert.ToInt32(DateTime.Now.Subtract(InsuranceEndDate).TotalDays) <= 0;
    }
}
