namespace CompanyClaimsApi.Models
{
    public class AddClaimRequest
    {
        public string UCR { get; set; }
        public int CompanyID { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
