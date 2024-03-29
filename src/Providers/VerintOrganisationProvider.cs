﻿namespace organisation_service.Providers;

public class VerintOrganisationProvider : IOrganisationProvider
{
    public EOrganisationProvider ProviderName => EOrganisationProvider.CRM;

    private readonly IVerintServiceGateway _verintServiceGateway;

    public VerintOrganisationProvider(IVerintServiceGateway verintServiceGateway) => _verintServiceGateway = verintServiceGateway;
    public async Task<IEnumerable<OrganisationSearchResult>> SearchAsync(string organisation)
    {
        var result = await _verintServiceGateway.SearchForOrganisationByName(organisation);
        return result.ResponseContent;
    }
}