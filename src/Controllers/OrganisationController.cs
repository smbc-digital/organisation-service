namespace organisation_service.Controllers;

[Produces("application/json")]
[Route("api/v1/[Controller]")]
[ApiController]
[TokenAuthentication]
public class OrganisationController : ControllerBase
{
    private readonly IOrganisationService _organisationService;

    public OrganisationController(IOrganisationService organisationService) => _organisationService = organisationService;

    [HttpGet]
    [Route("{organisationProvider}/{searchTerm}")]
    public async Task<IActionResult> Get(EOrganisationProvider organisationProvider, string searchTerm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var result = await _organisationService.SearchAsync(organisationProvider, searchTerm);
            return Ok(result);
        }
        catch (ProviderException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}