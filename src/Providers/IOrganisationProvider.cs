using System.Collections.Generic;
using System.Threading.Tasks;
using StockportGovUK.NetStandard.Models.Enums;
using StockportGovUK.NetStandard.Models.Verint.Lookup;

namespace organisation_service.Providers
{
    public interface IOrganisationProvider
    {
        EOrganisationProvider ProviderName { get; }

        Task<IEnumerable<OrganisationSearchResult>> SearchAsync(string organisation);
    }
}