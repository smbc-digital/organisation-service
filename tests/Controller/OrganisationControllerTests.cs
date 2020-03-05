using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using organisation_service.Controllers;
using organisation_service.Exceptions;
using organisation_service.Services;
using StockportGovUK.NetStandard.Models.Enums;
using StockportGovUK.NetStandard.Models.Verint.Lookup;
using Xunit;

namespace organisation_service_tests.Controllers
{
    public class OrganisationControllerTests
    {
        [Fact]
        public async Task Get_ShouldCallOrganisationService_AndReturnSingleItem()
        {
            Mock<IOrganisationService> mockService = new Mock<IOrganisationService>();
            var controller = new OrganisationController(mockService.Object);
            mockService.Setup(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()))
                .ReturnsAsync(new List<OrganisationSearchResult> {new OrganisationSearchResult()});

            var response = await controller.Get(EOrganisationProvider.Fake, "test");

            Assert.IsType<OkObjectResult>(response);
            mockService.Verify(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()),Times.Once);
        }

        [Fact]
        public void Get_ShouldCallOrganisationService_AndError()
        {
            Mock<IOrganisationService> mockService = new Mock<IOrganisationService>();
            var controller = new OrganisationController(mockService.Object);
            mockService.Setup(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()))
                .Throws(new ProviderException("SearchAsync: No provider selected to perform search operation"));

            Assert.ThrowsAsync<ProviderException>(() => controller.Get(EOrganisationProvider.Unknown, "test"));
        }
    }
}
