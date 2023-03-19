using Castle.Components.DictionaryAdapter;
using CompanyClaimsApi.Data;
using CompanyClaimsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace CompanyClaimsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : Controller
    {
        private readonly ClaimsAPIDbContext dbContext;

        public ClaimsController(ClaimsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            if (dbContext.Claims.Count() == 0)

            {
                Claims claim = new Claims();
                claim.Id = Guid.NewGuid();
                claim.UCR = "test";
                claim.CompanyID = 1;
                claim.ClaimDate = DateTime.Parse("7/7/2020");
                claim.LossDate = DateTime.Parse("1/2/2023");
                claim.AssuredName = "test1";
                claim.IncurredLoss = 12.23m;
                claim.Closed = false;
                dbContext.Claims.Add(claim);
                dbContext.SaveChanges();

                claim.Id = Guid.NewGuid();
                claim.UCR = "test2";
                claim.CompanyID = 2;
                claim.ClaimDate = DateTime.Parse("3/3/2022");
                claim.LossDate = DateTime.Parse("2/2/2022");
                claim.AssuredName = "test2";
                claim.IncurredLoss = 23.48m;
                claim.Closed = false;
                dbContext.Claims.Add(claim);
                dbContext.SaveChanges();

                claim.Id = Guid.NewGuid();
                claim.UCR = "test3";
                claim.CompanyID = 2;
                claim.ClaimDate = DateTime.Parse("1/5/2022");
                claim.LossDate = DateTime.Parse("4/5/2022");
                claim.AssuredName = "test3";
                claim.IncurredLoss = 1233.43m;
                claim.Closed = false;

                dbContext.Claims.Add(claim);
                dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClaims()
        {
            return Ok(await dbContext.Claims.ToListAsync());

        }
        /// <summary>
        /// Select details of a claim and how old the claim is in days
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClaimsById([FromRoute] Guid id)
        {
            try
            {
                var claim = await dbContext.Claims.FindAsync(id);

                if (claim != null)
                {
                  return Ok(claim);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        /// <summary>
        /// Select list of Claims by Company Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{companyid:int}")]
        public async Task<IActionResult> GetClaimsByCompanyId([FromRoute] int companyid)
        {
            try
            {
                var ClaimsByCompanyList = await dbContext.Claims.Where(ClaimsByCompany => ClaimsByCompany.CompanyID == companyid).ToListAsync();
                return Ok(ClaimsByCompanyList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClaim(AddClaimRequest addClaimRequest)
        {
            try
            {
                ValidateCompanyAdd(addClaimRequest);

            var claim = new Claims()
            {
                Id = Guid.NewGuid(),
                UCR = addClaimRequest.UCR,
                CompanyID = addClaimRequest.CompanyID,
                ClaimDate = addClaimRequest.ClaimDate,
                LossDate = addClaimRequest.LossDate,
                AssuredName = addClaimRequest.AssuredName,
                IncurredLoss = addClaimRequest.IncurredLoss,
                Closed = addClaimRequest.Closed
            };

            await dbContext.Claims.AddAsync(claim);
            await dbContext.SaveChangesAsync();

            return Ok(claim);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateClaim([FromRoute] Guid id,UpdateClaimRequest updateClaimRequest)
        {
            try
            {
                ValidateCompanyUpdate(updateClaimRequest);

                var claim = await dbContext.Claims.FindAsync(id);

                if (claim != null)
                {
                    claim.UCR = updateClaimRequest.UCR;
                    claim.CompanyID = updateClaimRequest.CompanyID;
                    claim.ClaimDate = updateClaimRequest.ClaimDate;
                    claim.LossDate = updateClaimRequest.LossDate;
                    claim.AssuredName = updateClaimRequest.AssuredName;
                    claim.IncurredLoss = updateClaimRequest.IncurredLoss;
                    claim.Closed = updateClaimRequest.Closed;

                    await dbContext.SaveChangesAsync();

                    return Ok(claim);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.InnerException);
            }
        }

        private void ValidateCompanyAdd(AddClaimRequest addClaimRequest)
        {
            if (addClaimRequest.UCR.Length > 20) { throw new Exception("UCR Length too long"); }

        }
        private void ValidateCompanyUpdate(UpdateClaimRequest updateClaimRequest)
        {
            if (updateClaimRequest.UCR.Length > 20) { throw new Exception("UCR Length too long"); }
 
        }
    }
}
