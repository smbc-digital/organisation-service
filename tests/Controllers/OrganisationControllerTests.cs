namespace organisation_service_tests.Controllers;

public class OrganisationControllerTests
{
    private readonly Mock<IOrganisationService> _mockService = new();

    private readonly OrganisationController _controller;

    public OrganisationControllerTests() => _controller = new OrganisationController(_mockService.Object);

    [Fact]
    public async Task Get_ShouldCallOrganisationService()
    {
        _mockService
            .Setup(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()))
            .ReturnsAsync(new List<OrganisationSearchResult> {new()});

        await _controller.Get(EOrganisationProvider.Fake, "test");

        _mockService.Verify(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Get_ShouldReturnOkResult()
    {
        _mockService
            .Setup(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()))
            .ReturnsAsync(new List<OrganisationSearchResult> { new() });

        var response = await _controller.Get(EOrganisationProvider.Fake, "test");

        var okObjectResult = Assert.IsType<OkObjectResult>(response);
        Assert.IsAssignableFrom<IEnumerable<OrganisationSearchResult>>(okObjectResult.Value);
    }

    [Fact]
    public async Task Get_ReturnBadRequest_IfProviderExceptionThrown()
    {
        _mockService.Setup(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()))
            .Throws(new ProviderException("SearchAsync: No provider selected to perform search operation"));

        var result = await _controller.Get(EOrganisationProvider.Unknown, "test");

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_Return500_IfExceptionThrown()
    {
        _mockService.Setup(service => service.SearchAsync(It.IsAny<EOrganisationProvider>(), It.IsAny<string>()))
            .Throws(new Exception("SearchAsync: No provider selected to perform search operation"));

        var result = await _controller.Get(EOrganisationProvider.Unknown, "test");

        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
}