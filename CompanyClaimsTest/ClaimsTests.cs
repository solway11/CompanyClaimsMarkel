using CompanyClaimsApi.Controllers;
using CompanyClaimsApi.Data;
using CompanyClaimsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace CompanyClaimsTest
{
    public class ClaimsTests
    {
        public ClaimsTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClaimsAPIDbContext>();
            optionsBuilder.UseInMemoryDatabase("ClaimsDb");
            this.dbContext = new ClaimsAPIDbContext(optionsBuilder.Options);
        }

        private readonly ClaimsAPIDbContext dbContext;

        [Fact]
        public async Task FiveHundredErrorWhenAddingAClaimWithUCRMoreThan20Chars()
        {
            var claimsController = new ClaimsController(this.dbContext);
            var addClaimRequest = new AddClaimRequest {                
                UCR = "test",
                CompanyID = 2,
                ClaimDate = DateTime.Parse("1/5/2022"),
                LossDate = DateTime.Parse("4/5/2022"),
                AssuredName = "test3",
                IncurredLoss = 1233.43m,
                Closed = false
            };

            // Act
            var result = await claimsController.AddClaim(addClaimRequest) as OkObjectResult;
            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.StatusCode,200); 
        }
    }
}