using System.Data.Common;

namespace IntegrationTests.Database
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
