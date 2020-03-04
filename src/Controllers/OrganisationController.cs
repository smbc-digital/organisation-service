using System.Threading.Tasks;
using organisation_service.Utils.Toggles;
using Microsoft.AspNetCore.Mvc;
using StockportGovUK.AspNetCore.Attributes.TokenAuthentication;
using StockportGovUK.AspNetCore.Availability.Attributes;
using StockportGovUK.AspNetCore.Availability.Managers;

namespace organisation_service.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [TokenAuthentication]
    //[OperationalToggle(OperationalToggles.organisation_service)]
    public class OrganisationController : ControllerBase
    {
        private IAvailabilityManager _availabilityManager;


        public OrganisationController(IAvailabilityManager availabilityManager)
        {
            _availabilityManager = availabilityManager;
        }

        [HttpGet]
        // [FeatureToggle(FeatureToggles.MyToggle)]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        // [FeatureToggle(FeatureToggles.MyToggle)]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}