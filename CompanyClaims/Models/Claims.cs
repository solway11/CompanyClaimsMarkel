using Castle.Components.DictionaryAdapter;
using CompanyClaimsApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CompanyClaimsApi.Models
{

    public class Claims
    {
        public Guid Id { get; set; }
        [Required]
        public string UCR { get; set; }
        public int CompanyID { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int DaysOld => Convert.ToInt32(DateTime.Now.Subtract(ClaimDate).TotalDays);
                               
    }
}
