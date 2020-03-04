using organisation_service.Controllers;
using Moq;
using StockportGovUK.AspNetCore.Availability.Managers;

namespace organisation_service_tests.Controllers
{
    public class OrganisationControllerTests
    {
        private readonly OrganisationController _organisationController;
        private readonly Mock<IAvailabilityManager> _mockAvailabilityManager = new Mock<IAvailabilityManager>();

        public OrganisationControllerTests()
        {
            _organisationController = new OrganisationController(_mockAvailabilityManager.Object);
        }
    }
}
