using IntegrationTests.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests
{
    public class BaseTest : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly CustomWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public BaseTest(DatabaseFixture fixture)
        {
            this._fixture = fixture;
            this._factory = new CustomWebApplicationFactory(_fixture.Database.GetConnection());
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
