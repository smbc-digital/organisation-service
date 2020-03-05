using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using organisation_service.Exceptions;
using organisation_service.Providers;
using StockportGovUK.NetStandard.Models.Enums;
using StockportGovUK.NetStandard.Models.Verint.Lookup;

namespace organisation_service.Services
{
    public interface IOrganisationService
    {
        Task<IEnumerable<OrganisationSearchResult>> SearchAsync(EOrganisationProvider organisationProvider, string searchTerm);
    }

    public class OrganisationService : IOrganisationService
    {
        private readonly IEnumerable<IOrganisationProvider> _organisationProviders;
        private readonly ILogger<IOrganisationProvider> _logger;

        public OrganisationService(IEnumerable<IOrganisationProvider> organisationProviders, ILogger<IOrganisationProvider> logger)
        {
            _organisationProviders = organisationProviders;
            _logger = logger;
        }

        public async Task<IEnumerable<OrganisationSearchResult>> SearchAsync(EOrganisationProvider organisationProvider, string searchTerm)
        {
            var provider = _organisationProviders
                .ToList()
                .FirstOrDefault(_ => _.ProviderName == organisationProvider);

            switch (organisationProvider)
            {
                case EOrganisationProvider.CRM:
                    return await provider.SearchAsync(searchTerm);
                case EOrganisationProvider.Fake:
                    return await provider.SearchAsync(searchTerm);
                default:
                    throw new ProviderException("SearchAsync: No provider selected to perform search operation");
            }
        }
    }
}
