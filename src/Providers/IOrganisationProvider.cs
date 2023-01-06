namespace organisation_service.Providers;

public interface IOrganisationProvider
{
    EOrganisationProvider ProviderName { get; }

    Task<IEnumerable<OrganisationSearchResult>> SearchAsync(string organisation);
}