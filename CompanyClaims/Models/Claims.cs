using Castle.Components.DictionaryAdapter;
using CompanyClaimsApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CompanyClaimsApi.Models
{

    public class Claims
    {
        [Required]
        public Guid Id { get; set; }
        public string UCR { get; set; }
        public int CompanyID { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        [Range(0, 9999999999999.99)]
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int DaysOld => Convert.ToInt32(DateTime.Now.Subtract(ClaimDate).TotalDays);                               
    }
}
