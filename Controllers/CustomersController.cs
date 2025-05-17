using Koperasi_Tentera_WebApi.Model;
using Koperasi_Tentera_WebApi.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Koperasi_Tentera_WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private AppDbContext _dbContext;
        public CustomersController(AppDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string icNumber)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.ICNumber == icNumber);

            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest("Account not found.");
            }
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = _dbContext.Customers.FirstOrDefault(x => x.ICNumber == customer.ICNumber);

            if (existingCustomer == null)
            {
                _dbContext.Customers.Add(new Customer
                {
                    CustomerName = customer.CustomerName,
                    ICNumber = customer.ICNumber,
                    MobileNumber = customer.MobileNumber,
                    EmailAddress = customer.EmailAddress,
                }
                );
                _dbContext.SaveChanges();
            }
            else
            {
                return BadRequest("Account already exist");
            }

            return Ok();
        }

        [HttpPost]
        [Route("VerifyEmail")]
        public IActionResult VerifyEmail(string icNumber, string emailOTP)
        {   
            var isEmailOTPMatched = _dbContext.Customers.Any(x => x.ICNumber == icNumber && x.OTPEmail == emailOTP);

            if (isEmailOTPMatched)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Incorrect OTP");
            }     
        }

        [HttpPost]
        [Route("VerifyPhone")]
        public IActionResult VerifyPhone(string icNumber, string phoneOTP)
        {
            var isEmailOTPMatched = _dbContext.Customers.Any(x => x.ICNumber == icNumber && x.OTPPhone == phoneOTP);

            if (isEmailOTPMatched)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Incorrect OTP");
            }
        }

        [HttpPost]
        [Route("CreatePin")]
        public IActionResult CreatePin(string icNumber, string inputPIN)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(x => x.ICNumber == icNumber);

            if (existingCustomer != null)
            {
                existingCustomer.Pin = inputPIN;
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Account not found.");
            }
        }

        [HttpPost]
        [Route("VerifyPin")]
        public IActionResult VerifyPin(string icNumber, string inputPIN)
        {
            var isPinMatched = _dbContext.Customers.Any(x => x.ICNumber == icNumber && x.Pin == inputPIN);

            if (isPinMatched)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Unmatched PIN.");
            }
        }
    }
}
