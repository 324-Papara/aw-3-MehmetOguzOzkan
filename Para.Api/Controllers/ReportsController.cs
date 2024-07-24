using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Para.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly string _connectionString;

        public ReportingController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        [HttpGet("customer-report")]
        public async Task<IActionResult> GetCustomerReport()
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT 
                        c.Id, c.FirstName, c.LastName, c.IdentityNumber, c.Email, c.CustomerNumber, c.DateOfBirth,
                        cd.FatherName, cd.MotherName, cd.EducationStatus, cd.MonthlyIncome, cd.Occupation,
                        ca.Country, ca.City, ca.AddressLine, ca.ZipCode, ca.IsDefault AS IsDefaultAddress,
                        cp.CountyCode, cp.Phone, cp.IsDefault AS IsDefaultPhone
                    FROM 
                        Customer c
                    LEFT JOIN 
                        CustomerDetail cd ON c.Id = cd.CustomerId
                    LEFT JOIN 
                        CustomerAddress ca ON c.Id = ca.CustomerId
                    LEFT JOIN 
                        CustomerPhone cp ON c.Id = cp.CustomerId
                    WHERE 
                        c.IsActive = 1
                ";

                var customerReports = await db.QueryAsync<CustomerReport>(sql);

                return Ok(customerReports);
            }
        }
    }

    public class CustomerReport
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EducationStatus { get; set; }
        public string MonthlyIncome { get; set; }
        public string Occupation { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefaultAddress { get; set; }
        public string CountyCode { get; set; }
        public string Phone { get; set; }
        public bool IsDefaultPhone { get; set; }
    }
}
