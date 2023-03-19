namespace CompanyClaimsApi.Models
{
    public class SelectClaimRequest
    {
        public Guid Id { get; set; }        
        public string UCR { get; set; }
        public int CompanyID { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public string IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int DaysOld { get; set; }
    }
}
