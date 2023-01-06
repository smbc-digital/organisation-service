namespace organisation_service.Providers;

public class FakeOrganisationProvider : IOrganisationProvider
{
    public EOrganisationProvider ProviderName => EOrganisationProvider.Fake;
    public async Task<IEnumerable<OrganisationSearchResult>> SearchAsync(string organisation) => await Task.FromResult(new List<OrganisationSearchResult> {
        new() {
            Name = "Organisation 1",
            Reference = "0101010101"
        },
        new() {
            Name = "Organisation 2",
            Reference = "0202020202",
            Address = "1 street, city, sk11aa"
        },
        new() {
            Name = "Organisation 3",
            Reference = "030303030303",
            Address = "3 town hall lane, city, sk11aa"
        }
    });
}