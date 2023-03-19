using CompanyClaimsApi.Data;
using CompanyClaimsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System.Security.Claims;

namespace CompanyClaimsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
       
        private readonly ClaimsAPIDbContext dbContext;

        public CompanyController(ClaimsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;

            if (dbContext.Company.Count() == 0)
            {
                Company company = new Company();
                company.Id = 1;
                company.Name = "Microsoft";
                company.Address1 = "90750 Carpenter Road";
                company.Address2 = "Apt 1961";
                company.Address3 = "Suite 29";
                company.Postcode = "75712 CEDEX 15";
                company.Country = "USA";
                company.Active = true;
                company.InsuranceEndDate = DateTime.Parse("1/1/2024");

                dbContext.Company.Add(company);
                dbContext.SaveChanges();

                company.Id = 2;
                company.Name = "Ikea";
                company.Address1 = "38793 Old Gate Plaza";
                company.Address2 = "Suite 90";
                company.Address3 = "12th Floor";
                company.Postcode = "289 34";
                company.Country = "Sweden";
                company.Active = true;
                company.InsuranceEndDate = DateTime.Parse("4/5/2022");

                dbContext.Company.Add(company);
                dbContext.SaveChanges();

                company.Id = 3;
                company.Name = "Google";
                company.Address1 = "2622 Ridge Oak Junction";
                company.Address2 = "Packers Way";
                company.Address3 = "Room 1462";
                company.Postcode = "S4 8RT";
                company.Country = "UK";
                company.Active = true;
                company.InsuranceEndDate = DateTime.Parse("4/5/2023");

                dbContext.Company.Add(company);
                dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            return Ok(await dbContext.Company.ToListAsync());

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] int id)
        {
            bool bolActiveInsurancePolicy=false;

            try
            {
                var company = await dbContext.Company.FindAsync(id);

                if (company != null)
                {
                    
                    bolActiveInsurancePolicy = Convert.ToInt32(DateTime.Now.Subtract(company.InsuranceEndDate).TotalDays) <= 0;

                    var selectcompany = new SelectCompanyRequest()
                    {
                        Id = id,
                        Name = company.Name,
                        Address1 = company.Address1,
                        Address2 = company.Address2,
                        Address3 = company.Address3,
                        Postcode = company.Postcode,
                        Country = company.Country,
                        Active = company.Active,
                        InsuranceEndDate = company.InsuranceEndDate,
                        ActiveInsurancePolicy = bolActiveInsurancePolicy

                    };
                    return Ok(selectcompany);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
