namespace organisation_service.Services;

public interface IOrganisationService
{
    Task<IEnumerable<OrganisationSearchResult>> SearchAsync(EOrganisationProvider organisationProvider, string searchTerm);
}

public class OrganisationService : IOrganisationService
{
    private readonly IEnumerable<IOrganisationProvider> _organisationProviders;

    public OrganisationService(IEnumerable<IOrganisationProvider> organisationProviders) => _organisationProviders = organisationProviders;

    public async Task<IEnumerable<OrganisationSearchResult>> SearchAsync(EOrganisationProvider organisationProvider, string searchTerm)
    {
        var provider = _organisationProviders
            .ToList()
            .FirstOrDefault(_ => _.ProviderName == organisationProvider);

        return organisationProvider switch
        {
            EOrganisationProvider.CRM => await provider.SearchAsync(searchTerm),
            EOrganisationProvider.Fake => await provider.SearchAsync(searchTerm),
            _ => throw new ProviderException("SearchAsync: No provider selected to perform search operation")
        };
    }
}