using AdressValidation.Model;
using AdressValidation.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdressValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressValidationController : ControllerBase
    {
        private readonly IUSPSClient _uspsClient;

        public AddressValidationController(IUSPSClient uspsClient)
        {
            _uspsClient = uspsClient;
        }

        [HttpPost]
        public IActionResult ValidateAddress([FromBody] AddressModel address)
        {
            var isValid = _uspsClient.ValidateAddress(address);
            return Ok(new { isValid });
        }
    }
}
